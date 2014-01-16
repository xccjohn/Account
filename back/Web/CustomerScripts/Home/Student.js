function studentCtrl($scope, $http, $dialog, $location, $window) {

    $scope.noOfPages = 1;
    $scope.currentPage = 1;
    $scope.maxSize = 5;

    getData($http, $scope, { Page: $scope.currentPage, Rows: 10, Order: 'desc', OrderBy: 'CreateDate', Where: null });

    $scope.selectPage = function (pageNo) {
        getData($http, $scope, { Page: pageNo, Rows: 10, Order: 'desc', OrderBy: 'CreateDate', Where: null });
    };

    $scope.post = function () {
        //        $http.post('/home/AddOrUpdate', $scope.entity).success(function (response) {
        //            if (response) {
        //                window.location.reload();
        //            } else {
        //                alert(response.message);
        //            }
        //        });
        addOrUpdate();
    };

    $scope.clear = function () {
        for (var property in $scope.entity) {
            $scope.entity[property] = null;
        }
    };

    $scope.del = function (id) {
        //        var $tr = this;
        var title = 'delete alert';
        var msg = 'Do you sure to delete this record?';
        var btns = [{ result: 'ok', label: 'OK' }, { result: 'cancel', label: 'Cancel'}];

        $dialog.messageBox(title, msg, btns)
          .open()
          .then(function (result) {
              if (result === 'ok') {
                  $http.post('/home/Del/' + id).success(function (response) {
                      if (response.result === true) {
                          //$location.
                          //window.location.reload();
                          $window.location.reload();
                      } else {
                          //alert(response.message);
                          $window.alert(response.message);
                      }
                  });
              }
          });
    };

    $scope.edit = function (entity) {
        for (var property in $scope.entity) {
            $scope.entity[property] = entity[property];
        }
    };

    function addOrUpdate() {
        $http.post('/home/AddOrUpdate', $scope.entity).success(function (response) {
            if (response.result) {
                window.location.reload();
            } else {
                alert(response.message);
            }
        });
    }
}

function getData($http, $scope, pagingEntity) {
    $http.post('/home/Query', pagingEntity).success(function (response) {
        $scope.items = response.rows;
        $scope.currentPage = response.page;
        $scope.noOfPages = response.Pages;
    });
}

  