export OpenPetraOrgPath=/usr/local/openpetraorg/${OrgNameWithoutSpace}
export CustomerName="${OrgName}"
export userName=openpetra${OrgNameWithoutSpace}
export OPENPETRA_LocationPublicKeyFile=/var/www/patches/${OrgNameWithoutSpace}/publickey.xml
export OPENPETRA_LocationPrivateKeyFile=${OpenPetraOrgPath}/etc30/privatekey.xml
export OPENPETRA_RDBMSType=postgresql
export OPENPETRA_DBHOST=localhost
export OPENPETRA_DBPWD=${RandomDBPassword}
export OPENPETRA_DBUSER=petraserver_${OrgNameWithoutSpace}
export OPENPETRA_DBNAME=openpetra_${OrgNameWithoutSpace}
export OPENPETRA_DBPORT=5432
export OPENPETRA_PORT=${HostedPort}
export backupfile=$OpenPetraOrgPath/backup30/backup-`date +%Y%m%d`.sql.gz
export mono=/opt/novell/mono/bin/mono
