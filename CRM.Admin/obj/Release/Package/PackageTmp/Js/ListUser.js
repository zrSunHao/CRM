var listApp = angular.module("listApp", []);
listApp.controller('userCtrl', function ($scope, $http, $timeout, $location) {
    
    $scope.Users = [];
    $scope.user = {};
    $scope.name = "";
    $scope.pages = 1;//当前共几页
    $scope.dataNum = 0;//一共有多少条信息
    $scope.currentPage = 0;//当前显示的是第几页
    $scope.listsPerPage = 10;//设置每页显示个数 
    $scope.showPage = true;//页数信息是否显示
    $scope.ShowList = function () {
        $http({
            method: 'GET',
            url: '/User/ShowUserList'
        }).then(function successCallback(res) {
            $scope.Users = res.data;
            console.log(res);
        }, function errorCallback(res) {
            // 请求失败执行代码
        });
    }
    
    $scope.deleteUser = function (id) {
        $scope.deleteId = id;
        var r = confirm("确认删除")
        if (r == true) {
            //删除
            $http({
                method: 'post',
                url: '/User/DeleteUser?Id=' + id
            }).then(function successCallback(res) {
                //返回一个数字判断是否大于零
                console.log(res.data);
                $timeout($scope.FindUser($scope.name), 1000);//删除后根据搜索内容再次刷新显示
            }, function errorCallback(response) {
                // 请求失败执行代码
            });
        }
        else {
            //不删除
        }
       
    };
    $scope.FindUser = function (name) {
        if (name == null || name == "") {
            $scope.getPageData($scope.currentPage);
            $scope.showPage = true;//页数信息设置显示
        } else {
            $http({
                method: 'get',
                url: '/User/FindUser?name=' + name
            }).then(function successCallback(res) {
                $scope.Users = res.data;
                console.log(res);
            }, function errorCallback(response) {
                // 请求失败执行代码
                });
            $scope.showPage = false;//页数信息设置不显示
        }
    }
    //得到信息总数，根据信息总数得到显示页数
    $scope.getUserNumSize = function () {
        $http({
            method: 'get',
            url: '/User/GetUserNumSize'
        }).then(function successCallback(res) {
            $scope.dataNum = res.data;
            var i = $scope.dataNum / $scope.listsPerPage;
            $scope.pages = Math.floor(i);
            var i = $scope.dataNum % $scope.listsPerPage;
            if (i > 0) {
                $scope.pages++;
            }
            console.log(res.data);
        }, function errorCallback(response) {
            // 请求失败执行代码
        });
    }


    //上一页  
    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            $scope.currentPage--;
        }
        console.log($scope.currentPage);
        $scope.getPageData($scope.currentPage);
    }
    //下一页  
    $scope.nextPage = function () {
        if ($scope.currentPage < $scope.pages - 1) {
            $scope.currentPage++;
        }
        console.log($scope.currentPage);
        $scope.getPageData($scope.currentPage);
    }  
    //向数据库中的查询语句
    $scope.getPageData = function (currentPage) {
        //发送查询语句
        $http({
            method: 'GET',
            url: '/User/ShowUserListPage?currentPage=' + currentPage
        }).then(function successCallback(res) {
            $scope.Users = res.data;
            console.log(res);
        }, function errorCallback(res) {
            // 请求失败执行代码
        });

    }
    //刷新显示页面信息列表--分页后
    $timeout($scope.getPageData($scope.currentPage), 100);
    //刷新显示分页信息--数据总量
    $timeout($scope.getUserNumSize, 101);

    //加载页面时刷新显示列表数据
    //$timeout($scope.ShowList, 100);

    //$scope.setShowList = function () {
        // $apply 方法可以修改数据
    //    $scope.$apply(function () {
    //        $http({
    //            method: 'GET',
    //            url: '/User/ShowUserList'
    //        }).then(function successCallback(res) {
    //            $scope.Users = res.data;
    //            console.log(res);
    //        }, function errorCallback(res) {
                // 请求失败执行代码
    //        });
    //    });
    //};


});


