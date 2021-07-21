(app => {
  app.config(["$stateProvider", "$locationProvider", function ($stateProvider, $locationProvider) {
    $stateProvider
      .state("openTab", {
        url: "/",
        templateUrl: "app/views/open-tab.html",
        controller: "OpenTabController",
        controllerAs: "model"
      })
      .state("document", {
        url: "/document",
        templateUrl: "app/views/document.html",
        controller: "DocumentController",
        controllerAs: "model",
        params: {
          tab: {}
        },
        resolve: {
          documents: ["$q", "$http", function ($q, $http) {

            const deferred = $q.defer();

            $http.get("api/tab/documents").then(function (response) {
              deferred.resolve(response.data);
            });

            return deferred.promise;
          }]
        }
      })
      .state("tabClosed", {
        url: "/tabClosed",
        templateUrl: "app/views/tab-closed.html",
        controller: "TabClosedController",
        controllerAs: "model",
        params: {
          tab: {}
        }
      });

    $locationProvider.hashPrefix("");
    $locationProvider.html5Mode({
      enabled: true, requireBase: false
    });
  }]);
})(app);