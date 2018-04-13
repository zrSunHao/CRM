var addApp = angular.module("addApp", ['ui.bootstrap', 'angularFileUpload']);
addApp.controller('userCtrl', function ($scope, $http, FileUploader, $timeout) {
    $scope.Id = 0;
    $scope.Name = null;
    $scope.Sex = true;
    $scope.Birthday = new Date();
    $scope.PhoneNumber = null;
    $scope.Address = "";
    $scope.Picture = "0.jpg";
    $scope.UploadImage = "";
    $scope.TempUploadImage = "";
    //日期控件 开始
    $scope.format = "yyyy/MM/dd";
    $scope.altInputFormats = ['yyyy/M!/d!'];
    $scope.popup1 = {
         opened: false
    };
    $scope.open1 = function () {
         $scope.popup1.opened = true;
    };
    //日期控件 结束
    var user = new Object();
    var uploader = $scope.uploader = new FileUploader({
        url: '/User/UploadAddImage'
    });
    uploader.onSuccessItem = function (fileItem, response, status, headers) {
        alert("图片上传成功!");
    };
    uploader.onErrorItem = function (fileItem, response, status, headers) {
        alert("图片上传失败!");
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
    $scope.AddUser = function () {
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
            url: '/User/AddUser'
        }).then(function successCallback(res) {
            alert("添加成功");
            //window.location.href = "/User/ListUser";//添加成功，跳转到列表页面
        }, function errorCallback(response) {
            // 请求失败执行代码
        });
    };
});
