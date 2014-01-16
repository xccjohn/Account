
//angular.module('view', ['ui.bootstrap']).directive('ngClear', function () {
//    return function (scope, element) {
//        scope.clear = function () {
//            for (var property in scope.entity) {
//                scope.entity[property] = null;
//            }
//        };

//        element.bind('click', function () {
//            scope.clear();
//        });
//    }
//});

var View = function () { };

(function () {

    View.prototype.grid = function (option) {
        var options = $.extend({
            multiselect: false,
            multiboxonly: false,
            checkMultiboxWhenSelect: false,
            onSelectRow: function (x, d) { },
            onCheckRow: null,
            onCheckAll: null,
            resizable: true,
            keyColumn: 'Id'
        }, option || {}),
            gridElement = options.gridElement,
            pagerElement = options.pagerElement,
            headerElement = null,
            $container = $(options.container);

        if (options.container) {
            if (options.container.gridElement) {
                gridElement = $('#' + options.container.gridElement);
                pagerElement = $('#' + options.container.pagerElement);
            }
            else {
                gridElement = html(options.container, '<table>');
                pagerElement = html(options.container, '<div>');
                // jqGrid requires that the elements be selected with a real selector.
                gridElement = $('#' + gridElement.id);
                pagerElement = $('#' + pagerElement.id);
            }
        }
        buildGrid(gridElement, pagerElement, options);

        headerElement = $('.ui-jqgrid-hdiv', $container);
        return gridElement;

        function buildGrid(gridElement, pagerElement, options) {
            var gridObj = gridElement.jqGrid({
                autowidth: true,
                autoheight: true,
                viewrecords: true,
                recordpos: "left",
                url: options.url,
                datatype: "json",
                rowList: [20],
                height: options.height? options.height:460,
                width:600,
                pager: pagerElement,
                colNames: options.colNames,
                colModel: options.colModels,
                multiselect: options.multiselect,
                multiboxonly: options.multiboxonly,
                jsonReader: { id: options.keyColumn, repeatitems: false }
            }).navGrid(pagerElement.selector, { edit: false, add: false, del: false, search: false, refresh: false });
        }

        function html(parent, html) {
            /// <summary>Creates HTML with a unique ID.</summary>
            return $(html, { id: _.uniqueId('x') }).appendTo(parent).get(0);
        };
    }

    window.view = new View();
})();





