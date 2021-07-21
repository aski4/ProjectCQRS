(app => {
  app.controller("DocumentController", ["TabService", "SignalRService", "$stateParams", "$state", "$scope", function (TabService, SignalRService, $stateParams, $state, $scope) {

    const vm = this;

    vm.tab = $stateParams.tab;
    vm.closeTab = closeTab;
    vm.postDocument = postDocument;
    vm.events = [];


    SignalRService.subscribeToNewEvent(function (event) {
      $scope.$apply(function() {
        vm.events.push(event);
      });
    });


    function closeTab(finaltext) {
        TabService.closeTab(vm.tab.id, finaltext, () => getCurrentTabDetails(tab => $state.go("tabClosed", { tab: tab })));
    }

      function postDocument() {

          console.log($scope);
          TabService.postDocument(vm.tab.id, $scope.myFile, () => refreshTabDetails());
      }
    
    function refreshTabDetails() {
      getCurrentTabDetails(tab => vm.tab = tab);
    }

    function getCurrentTabDetails(success) {
      return TabService.getTabDetails(vm.tab.id, success);
    }

  }]);
})(app);