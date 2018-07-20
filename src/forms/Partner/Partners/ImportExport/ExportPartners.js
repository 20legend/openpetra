function UploadUserCSV(self) {

	var filename = self.val();

	// see http://www.html5rocks.com/en/tutorials/file/dndfiles/
	if (window.File && window.FileReader && window.FileList && window.Blob) {
		//alert("Great success! All the File APIs are supported.");
	} else {
	  alert('The File APIs are not fully supported in this browser.');
	}

	var reader = new FileReader();

	reader.onload = function (event) {
		p = {'ACSVPartnerData': event.target.result, 'ADateFormat': "dmy", "ASeparator": ";"};

		api.post('serverMPartner.asmx/TImportExportWebConnector_ImportFromCSVFile', p)
		.then(function (result) {
			result = JSON.parse(result.data.d);
			result = result.result;
			if (result == true) {
				display_message(i18next.t('ImportAndExportDatabase.upload_partner_success'), "success");
			} else {
				display_message(i18next.t('ImportAndExportDatabase.upload_partner_fail'), "fail");
			}
		})
		.catch(error => {
			//console.log(error.response)
			display_message(i18next.t('ImportAndExportDatabase.upload_partner_fail'), "fail");
		});

	}
	// Read in the file as a data URL.
	reader.readAsText(self[0].files[0]);

};
