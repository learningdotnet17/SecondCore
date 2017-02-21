//site.js

(function () {

    var ele = $("#username");
    ele.text("Tanya Chawla");

    var main = $("#main");
    main.on("mouseenter", function () {
        main.style = 'background-color: #800;';
    });
    main.on("mouseleave", function () {
        main.style = "";
    });

    //var menuitems = $("ul.menu li a");
    //menuitems.on("click", function () {
    //    var me = $(this);
    //    alert(me.text());
    //});

})();
