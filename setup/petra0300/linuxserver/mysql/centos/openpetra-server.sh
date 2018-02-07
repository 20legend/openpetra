#!/bin/sh
#
# description: Starts and stops the OpenPetra server running with Mono
#

export OpenPetraPath=/usr/local/openpetra
export documentroot=/var/www/openpetra
export OPENPETRA_DBPORT=3306
export OPENPETRA_RDBMSType=mysql

generatepwd() {
  dd bs=1024 count=1 if=/dev/urandom status=none | tr -dc 'a-zA-Z0-9#?_' | fold -w 32 | head -n 1
}

if [ -z "$NAME" ]
then
  export NAME=openpetra-server
  export userName=openpetra
  export OPENPETRA_DBUSER=petraserver
  export OPENPETRA_DBNAME=openpetra
  export OPENPETRA_PORT=9000
  export OPENPETRA_DBHOST=localhost
fi

if [ -z "$backupfile" ]
then
  export backupfile=/home/$userName/backup/backup-`date +%Y%m%d`.sql.gz
fi

if [ "$2" != "" ]
then
  backupfile=$2
  useremail=$2
  ymlgzfile=$2
fi

# start the openpetra server
start() {
    echo "Starting OpenPetra server"
    if [ "`whoami`" = "$userName" ]
    then
      cd /var/www/openpetra
      LD_LIBRARY_PATH=$LD_LIBRARY_PATH:$OpenPetraPath/bin30 fastcgi-mono-server4 /socket=tcp:127.0.0.1:$OPENPETRA_PORT /applications=/:$documentroot /appconfigfile=/home/$userName/etc/PetraServerConsole.config /logfile=/home/$userName/log/mono.log /loglevels=Standard >& /dev/null
      # other options for loglevels: Debug Notice Warning Error Standard(=Notice Warning Error) All(=Debug Standard)
    else
      echo "Error: can only start the server as user $userName"
      exit -1
    fi
}

# stop the openpetra server
stop() {
    echo "Stopping OpenPetra server"
    if [ "`whoami`" = "$userName" ]
    then
      cd $OpenPetraPath/bin30; mono --runtime=v4.0 --server PetraServerAdminConsole.exe -C:/home/$userName/etc/PetraServerAdminConsole.config -Command:Stop
    else
      echo "Error: can only stop the server as user $userName"
      exit -1
    fi
}

# load a new database from a yml.gz file. this will overwrite the current database!
loadYmlGz() {
    su - $userName -c "cd $OpenPetraPath/bin30; mono --runtime=v4.0 Ict.Petra.Tools.MSysMan.YmlGzImportExport.exe -C:/home/$userName/etc/PetraServerConsole.config -Action:load -YmlGzFile:$ymlgzfile"
}

# dump the database to a yml.gz file
dumpYmlGz() {
    su - $userName -c "cd $OpenPetraPath/bin30; mono --runtime=v4.0 Ict.Petra.Tools.MSysMan.YmlGzImportExport.exe -C:/home/$userName/etc/PetraServerConsole.config -Action:dump -YmlGzFile:$ymlgzfile"
}

# display a menu to check for logged in users etc
menu() {
    su - $userName -c "cd $OpenPetraPath/bin30; mono --runtime=v4.0 --server PetraServerAdminConsole.exe -C:/home/$userName/etc/PetraServerAdminConsole.config"
}

# backup the mysql database
backup() {
    echo `date` "Writing to " $backupfile
    su $userName -c "mysqldump --host=$OPENPETRA_DBHOST --port=$OPENPETRA_DBPORT --user=$OPENPETRA_DBUSER $OPENPETRA_DBNAME | gzip > $backupfile"
    echo `date` "Finished!"
}

# restore the mysql database
restore() {
    echo "This will overwrite your database!!!"
    echo "Please enter 'yes' if that is ok:"
    read response
    if [ "$response" != 'yes' ]
    then
        echo "Cancelled the restore"
        exit
    fi

    echo `date` "Start restoring from " $backupfile
    echo "creating database..."

    echo "DROP DATABASE IF EXISTS \`$OPENPETRA_DBNAME\`;" > $OpenPetraPath/tmp30/createtables-MySQL.sql
    echo "CREATE DATABASE IF NOT EXISTS \`$OPENPETRA_DBNAME\`;" >> $OpenPetraPath/tmp30/createtables-MySQL.sql
    echo "USE \`$OPENPETRA_DBNAME\`;" >> $OpenPetraPath/tmp30/createtables-MySQL.sql
    cat $OpenPetraPath/db30/createtables-MySQL.sql >> $OpenPetraPath/tmp30/createtables-MySQL.sql
    echo "GRANT ALL ON \`$OPENPETRA_DBNAME\`.* TO \`$OPENPETRA_DBUSER\` IDENTIFIED BY '$OPENPETRA_DBPWD'" >> $OpenPetraPath/tmp30/createtables-MySQL.sql
    mysql -u root --host=$OPENPETRA_DBHOST --port=$OPENPETRA_DBPORT < $OpenPetraPath/tmp30/createtables-MySQL.sql
    rm $OpenPetraPath/tmp30/createtables-MySQL.sql

    echo "loading data..."
    echo $backupfile|grep -qE '\.gz$'
    if [ $? -eq 0 ]
    then
        cat $backupfile | gunzip | mysql -u $OPENPETRA_DBUSER --host=$OPENPETRA_DBHOST --port=$OPENPETRA_DBPORT $OPENPETRA_DBNAME > $OpenPetraPath/log30/mysqlload.log
    else
        mysql -u $OPENPETRA_DBUSER --host=$OPENPETRA_DBHOST --port=$OPENPETRA_DBPORT $OPENPETRA_DBNAME < $backupfile > $OpenPetraPath/log30/mysqlload.log
    fi

    echo "enabling indexes and constraints..."
    mysql -u $OPENPETRA_DBUSER --host=$OPENPETRA_DBHOST --port=$OPENPETRA_DBPORT $OPENPETRA_DBNAME < $OpenPetraPath/db30/createconstraints-MySQL.sql

    echo `date` "Finished!"
}

init() {
    if [ -z "$OPENPETRA_URL" ]
    then
      echo "please define the URL for your OpenPetra, eg. OPENPETRA_URL=demo.openpetra.org openpetra-server init"
      exit -1
    fi

    if [ -z "$OPENPETRA_DBPWD" ]
    then
      echo "please define a password for your OpenPetra database, eg. OPENPETRA_PWD=topsecret openpetra-server init"
      exit -1
    fi

    if [ -f /home/$userName/etc/PetraServerConsole.config ]
    then
      echo "it seems there is already an instance configured"
      exit -1
    fi

    echo "preparing OpenPetra server..."

    useradd --home /home/$userName $userName
    mkdir -p /home/$userName/log
    mkdir -p /home/$userName/tmp
    mkdir -p /home/$userName/etc
    mkdir -p /home/$userName/backup

    # copy config files (server, serveradmin.config) to etc, with adjustments
    cat $OpenPetraPath/etc30/PetraServerConsole.config \
       | sed -e "s/OPENPETRA_RDBMSType/$OPENPETRA_RDBMSType/" \
       | sed -e "s/OPENPETRA_DBHOST/$OPENPETRA_DBHOST/" \
       | sed -e "s/OPENPETRA_DBUSER/$OPENPETRA_DBUSER/" \
       | sed -e "s/OPENPETRA_DBNAME/$OPENPETRA_DBNAME/" \
       | sed -e "s/OPENPETRA_DBPORT/$OPENPETRA_DBPORT/" \
       | sed -e "s~PG_OPENPETRA_DBPWD~$OPENPETRA_DBPWD~" \
       | sed -e "s~OPENPETRA_URL~$OPENPETRA_URL~" \
       | sed -e "s/USERNAME/$userName/" \
       > /home/$userName/etc/PetraServerConsole.config
    cat $OpenPetraPath/etc30/PetraServerAdminConsole.config \
       | sed -e "s/USERNAME/$userName/" \
       | sed -e "s/openpetraOPENPETRA_PORT/$userName/" \
       > /home/$userName/etc/PetraServerAdminConsole.config
    OPENPETRA_HTTP_PORT=$((OPENPETRA_PORT-1000))

    chown -R $userName:$userName /home/$userName

    # configure nginx
    cat > /etc/nginx/conf.d/$userName.conf <<FINISH
server {
    listen $OPENPETRA_HTTP_PORT;
    server_name $OPENPETRA_URL;

    root /var/www/openpetra-client;

    location / {
         rewrite ^/Settings.*$ /;
         rewrite ^/Partner.*$ /;
         rewrite ^/Finance.*$ /;
         rewrite ^/CrossLedger.*$ /;
         rewrite ^/System.*$ /;
         rewrite ^/.git/.*$ / redirect;
         rewrite ^/etc/.*$ / redirect;
    }

    location /api {
         index index.html index.htm default.aspx Default.aspx;
         fastcgi_index Default.aspx;
         fastcgi_pass 127.0.0.1:$OPENPETRA_PORT;
         include /etc/nginx/fastcgi_params;
         sub_filter_types text/html text/css text/xml;
         sub_filter 'http://127.0.0.1:$OPENPETRA_PORT' 'https://$OPENPETRA_URL/api';
         sub_filter 'http://_/api' 'https://$OPENPETRA_URL/api';
    }
}
FINISH
    systemctl restart nginx
    systemctl enable nginx

    if [[ "$NAME" != "openpetra-server" ]]
    then
      # create the service script
      cp /usr/lib/systemd/system/openpetra-server.service /usr/lib/systemd/system/${NAME}.service
      sed -i "s~OpenPetra Server~OpenPetra Server for $userName~g" /usr/lib/systemd/system/${NAME}.service
      sed -i "s~User=openpetra~User=$userName\nEnvironment=NAME=$userName\nEnvironment=userName=$userName\nEnvironment=OPENPETRA_DBUSER=$OPENPETRA_DBUSER\nEnvironment=OPENPETRA_DBPWD='$OPENPETRA_DBPWD'\nEnvironment=OPENPETRA_DBHOST=$OPENPETRA_DBHOST\nEnvironment=OPENPETRA_DBPORT=$OPENPETRA_DBPORT\nEnvironment=OPENPETRA_DBNAME=$OPENPETRA_DBNAME\nEnvironment=OPENPETRA_PORT=$OPENPETRA_PORT~g" /usr/lib/systemd/system/${NAME}.service
    fi
    systemctl enable ${NAME}
    systemctl start ${NAME}

}

# this will overwrite all existing data
initdb() {
    if [ -z "$OPENPETRA_DBPWD" ]
    then
      echo "please define a password for your OpenPetra database, eg. OPENPETRA_PWD=topsecret openpetra-server init"
      exit -1
    fi

    echo "preparing OpenPetra database..."

    mkdir -p $OpenPetraPath/tmp30

    if [ "$OPENPETRA_DBHOST" == "localhost" ]
    then
      echo "initialise database"
      systemctl start mariadb
      systemctl enable mariadb
      echo "DROP DATABASE IF EXISTS \`$OPENPETRA_DBNAME\`;" > $OpenPetraPath/tmp30/createdb-MySQL.sql
      echo "CREATE DATABASE IF NOT EXISTS \`$OPENPETRA_DBNAME\`;" >> $OpenPetraPath/tmp30/createdb-MySQL.sql
      echo "USE \`$OPENPETRA_DBNAME\`;" >> $OpenPetraPath/tmp30/createdb-MySQL.sql
      echo "GRANT ALL ON \`$OPENPETRA_DBNAME\`.* TO \`$OPENPETRA_DBUSER\`@localhost IDENTIFIED BY '$OPENPETRA_DBPWD'" >> $OpenPetraPath/tmp30/createdb-MySQL.sql
      mysql -u root --host=$OPENPETRA_DBHOST --port=$OPENPETRA_DBPORT < $OpenPetraPath/tmp30/createdb-MySQL.sql
      rm -f $OpenPetraPath/tmp30/createdb-MySQL.sql
    fi
    echo "creating tables..."
    mysql -u $OPENPETRA_DBUSER --password="$OPENPETRA_DBPWD" --host=$OPENPETRA_DBHOST --port=$OPENPETRA_DBPORT $OPENPETRA_DBNAME < $OpenPetraPath/db30/createdb-MySQL.sql

    echo "initial data..."
    # insert initial data so that loadymlgz will work
    cat > $OpenPetraPath/tmp30/init-MySQL.sql <<FINISH
insert into s_user(s_user_id_c) values ('SYSADMIN');
insert into s_module(s_module_id_c) values ('SYSMAN');
insert into s_user_module_access_permission(s_user_id_c, s_module_id_c, s_can_access_l) values('SYSADMIN', 'SYSMAN', true);
insert into s_system_status (s_user_id_c, s_system_login_status_l) values ('SYSADMIN', true);
FINISH
    mysql -u $OPENPETRA_DBUSER --password="$OPENPETRA_DBPWD" --host=$OPENPETRA_DBHOST --port=$OPENPETRA_DBPORT $OPENPETRA_DBNAME < $OpenPetraPath/tmp30/init-MySQL.sql

    # load the clean database with sysadmin user but without ledger, partners etc
    ymlgzfile=$OpenPetraPath/db30/clean.yml.gz
    loadYmlGz

    # if url does not start with demo.
    if [[ ! $OPENPETRA_URL == demo.* ]]
    then
      mysql -u $OPENPETRA_DBUSER --password="$OPENPETRA_DBPWD" --host=$OPENPETRA_DBHOST --port=$OPENPETRA_DBPORT \
           -e "UPDATE s_user SET s_password_needs_change_l = 1 WHERE s_user_id_c = 'SYSADMIN'" $OPENPETRA_DBNAME
    fi

    echo "For production use, please change the password for user SYSADMIN immediately (initial password: CHANGEME)"
}

case "$1" in
    start)
        start
        ;;
    stop)
        stop
        ;;
    restart)
        stop
        sleep 3
        start
        ;;
    backup)
        backup
        ;;
    restore)
        restore
        ;;
    generatepwd)
        generatepwd
        ;;
    init)
        init
        ;;
    initdb)
        initdb
        ;;
    loadYmlGz)
        loadYmlGz
        ;;
    dumpYmlGz)
        dumpYmlGz
        ;;
    menu)
        menu
        ;;
    *)
        echo "Usage: $0 {start|stop|restart|menu|backup|restore|init|initdb|loadYmlGz|dumpYmlGz}"
        exit 1
        ;;
esac

exit 0
