var editApp = angular.module("editApp", ['ui.bootstrap', 'angularFileUpload']);
editApp.controller('userCtrl', function ($scope, $http, FileUploader, $timeout ) {
    $scope.Id = 0;
    $scope.Name = "";
    $scope.Sex = true;
    $scope.Birthday = new Date("2018-01-01");
    $scope.PhoneNumber = "";
    $scope.Address = "";
    $scope.Picture = "";
    //日期控件 开始
    $scope.format = "yyyy/MM/dd";
    //用户对象
    var user = new Object();
    $scope.altInputFormats = ['yyyy/M!/d!'];
    $scope.popup1 = {
        opened: false
    };
    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };
    //文件上传方法
    var uploader = $scope.uploader = new FileUploader({
        url: '/User/UploadAddImage'
    });
    uploader.onSuccessItem = function (fileItem, response, status, headers) {
        alert("上传成功!");
    };
    uploader.onErrorItem = function (fileItem, response, status, headers) {
        alert("上传失败!");
    };

    

    //日期控件 结束
    $scope.SubmitEdit = function () {
        if ($scope.Name == null || $scope.PhoneNumber == null) {
            alert("用户名与电话号码为必填项")
            return;
        }
        user.Id = $scope.Id;
        user.Name = $scope.Name;
        user.Sex = $scope.Sex;
        user.Birthday = $scope.Birthday;
        user.PhoneNumber = $scope.PhoneNumber;
        user.Address = $scope.Address;
        user.PictureUrl = $scope.Picture;
        var userJson = angular.toJson(user);
        uploader.uploadAll();
        $http({
            method: 'post',
            data: userJson,
            url: '/User/DOEdit'
        }).then(function successCallback(res) {
            var i = res.data;
            console.log(i);
            //先判断返回的数值，大于一修改成功，提示并跳转页面，小于一提示，并停留在页面
            if (i > 0) {
                alert("修改成功");
                window.location.href = "/User/ListUser"; 
            }
        }, function errorCallback(response) {
            // 请求失败执行代码
        });
    };
    $scope.GetEditUser = function () {
        $http({
            method: 'GET',
            url: '/User/GetEditUser'
        }).then(function successCallback(response) {
            // 请求成功执行代码
            console.log(response);
            user = response.data;
            $scope.Id = user.Id;
            $scope.Name = user.Name;
            $scope.Sex = user.Sex;
            $scope.Birthday = eval('new ' + (user.Birthday.replace(/\//g, '')));
            $scope.PhoneNumber = user.PhoneNumber;
            $scope.Address = user.Address;
            $scope.Picture = user.PictureUrl;
            console.log($scope.Picture);
            console.log(user);
        }, function errorCallback(response) {
            // 请求失败执行代码
        });

    };

    //上传文件变量
    $scope.files = null;
    $scope.ImageName = "";
    //点击上传文件按钮改变事件
    $scope.fileChanged = function (ele) {
        $scope.files = ele.files;
        $scope.$apply(); //传播Model的变化。
        console.log(ele.files[0].name);
        $scope.ImageName = ele.files[0].name;
        $scope.Picture = ele.files[0].name;
        alert("您要上传的文件名为：" + $scope.ImageName);
        //先将图片传入
        $timeout(uploader.uploadAll(), 100);
    }  
    
    $timeout($scope.GetEditUser, 100);
    
    

});