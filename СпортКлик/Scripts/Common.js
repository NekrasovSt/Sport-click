function Common() {
    var _this = this;
    this.init = function () {
        $(".LoginPopup").click(function () {
            _this.showPopup("/Account/Ajax", initLoginPopup);
        });
    }
    //Отправляем GET запрос в случае успеха вызываем showModalData
    this.showPopup = function (url, callback) {
        $.ajax({
            type: "GET",
            url: url,
            success: function (data) {
                showModalData(data, callback);
            }
        });
    }
    //При входе подписываемся на нажатие кнопки Вход.
    function initLoginPopup(modal) {
        $("#LoginButton").click(function () {
            $.ajax({
                type: "POST",
                url: "/Account/Ajax",
                //Отправляем пост запрос с формы "#LoginForm"
                data: $("#LoginForm").serialize(),
                success: function (data) {
                    showModalData(data);
                    initLoginPopup(modal);
                }
            });
        });
    }

    function showModalData(data, callback) {
        //Удаляет все совпавшие элементы из DOM
        $(".modal-backdrop").remove();
        var popupWrapper = $("#PopupWrapper");
        //Удаляет все дочерние блоки из каждого элемента в наборе совпавших элементов
        popupWrapper.empty();
        popupWrapper.html(data);
        var popup = $(".modal", popupWrapper);
        $(".modal", popupWrapper).modal('toggle');
        if (callback != undefined) {
            callback(popup);
        }
    }
}
//Автозаполнение для поиска продукта
function SearchProduct() {
    var _this = this;
    this.init = function () {
        $('#searchProduct').typeahead({
            source: function (частьИмени, process) {
                var url = '/Товар/ИскатьАвтозаполнение';
                //Отправляем запрос
                $.getJSON(url, { частьИмени: частьИмени }, function (data) {
                    process(data);
                });
            }
        });
    }
}
var common = null;
var searchProduct = null;
$().ready(function () {
    searchProduct = new SearchProduct();
    searchProduct.init();
    common = new Common();
    common.init();
});