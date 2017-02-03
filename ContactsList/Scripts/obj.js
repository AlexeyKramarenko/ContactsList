
var obj = (function () {

    var STATIC = {
        startRowIndex: 1
    };
    function getNextCompanyList(maximumRows) {

        var sortByExpression = $("#sortByExpression").val(),
            startRowIndex = ++STATIC.startRowIndex;

        $.ajax({
            url: '/default/GetNextCompanyList/' + maximumRows + "/" + startRowIndex + "/" + sortByExpression,
            type: 'GET',
            dataType: 'json',
            success: function (data) {

                var content = "";

                $.map(data.companiesVM, function (item, i) {

                    content += "<tr>" +
                                    "<td>" + item.Name + "</td>" +
                                    "<td>" + item.TownName + "</td>" +
                                    "<td>" + item.ActivityName + "</td>" +
                               "</tr>";
                });
                $("#companyList tbody").append(content);

                if (data.isLimit)
                    $("#btnShowMore").remove();
            },
            error: function (a, b, c) {
                console.log("error:" + a.statusText);
            }
        });
    }

    function sortRetrievedRows() {
        var sortByExpression = $("#sortByExpression").val(),
            rowCount = $('#companyList tr').length;

        $.ajax({
            url: '/default/GetSortedCompanyList/' + rowCount + "/" + sortByExpression,
            type: 'GET',
            dataType: 'json',
            success: function (data) {

                var content = "";

                $.map(data.companiesVM, function (item, i) {

                    content += "<tr>" +
                                     "<td>" + item.Name + "</td>" +
                                     "<td>" + item.TownName + "</td>" +
                                     "<td>" + item.ActivityName + "</td>" +
                               "</tr>";
                });
                $("#companyList tbody").html(content); 
            },
            error: function (a, b, c) {
                console.log("error:" + a.statusText);
            }
        });
    }

    return {
        getNextCompanyList : getNextCompanyList,
        sortRetrievedRows : sortRetrievedRows
    }
})();