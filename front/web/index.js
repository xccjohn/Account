/**
 *
 * @authors Your Name (you@example.org)
 * @date    2014-01-19 20:29:10
 * @version $Id$
 */

function index($scope, $http) {
	// body...

	$scope.list = [];

    $http.post('/db1/index',{publickey:''}).success(function(response) {
		$scope.list = response;
	});
}