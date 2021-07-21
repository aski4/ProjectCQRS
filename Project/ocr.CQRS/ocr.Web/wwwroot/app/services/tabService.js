(app => {
  app.factory("TabService", ["$http", "toastr", function ($http, toastr) {

    const baseUrl = "api/tab";

    return {
        openTab: openTab,
        closeTab: closeTab,
        documents: getDocuments,
        getTabDetails: getTabDetails,
        postDocument: postDocument,
        processDocument: processDocument
      };


    function openTab(tabId, clientName, success) {
      return $http.post(`${baseUrl}/open/${tabId}`, `"${clientName}"`)
        .then(success, showResponseErrors);
      }


    function closeTab(tabId, finaltext, success) {
      return $http.post(`${baseUrl}/close/${tabId}`, finaltext)
        .then(success, showResponseErrors);
      }


    function getDocuments(success) {
        return $http.get(`${baseUrl}/documents`)
            .then(response => success(response.data), showResponseErrors);
      }

      function postDocument(tabId, file, success) {
          var fd = new FormData();
          fd.append('file', file);
          return $http.post(`${baseUrl}/document/${tabId}`, fd, {
              transformRequest: angular.identity,
              headers: { 'Content-Type': undefined }
          })
              .then(success, showResponseErrors);
      }

    function getTabDetails(tabId, success) {
      return $http.get(`${baseUrl}/${tabId}`)
        .then(response => success(response.data), showResponseErrors);
      }

    function processDocument(tabId, success) {
          return $http.get(`${baseUrl}/document/processed/${tabId}`)
              .then(success, showResponseErrors);
      }

    function showResponseErrors(response) {
        const errors = response.data.messages;

        if (errors) {
            toastr.error(errors.join("; ", "Error"));
        }
      }

  }]);
})(app);