// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*
const { createApp } = Vue

createApp({
    data() {
        return {
            message: 'Hello Vue!'
        }
    }
}).mount('#full-page')
*/
$.when($.ready).then(function () {

    $(".menuL1-link").click(function (it) {
        var menuId = $(this).attr("data-menu-id");

        $("div.menuL1-Panel[data-menu-details-panel!='" + menuId + "']").hide();

        $("div.menuL1-Panel[data-menu-details-panel='" + menuId + "']").toggle();

    });

});