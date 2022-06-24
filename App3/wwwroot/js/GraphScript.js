
 //<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
/*<script type="text/javascript">*/

$(document).ready(function() {
    $.ajax({
        type: 'POST',
        url: '@Url.Action("Index")',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: '{}',
        success: function (respone) {
            alert("Hey i got fired on page load..");
            drawchart(); // calling method  
        },

        error: function (respone) {
            alert("Error loading data! Please try again.");
        }
    });
});



google.charts.load("current", { packages: ['corechart'] });
google.charts.setOnLoadCallback(drawChart);
function drawChart() {

    //var data = new google.visualization.DataTable();

    //data.addColumn('string', 'PlanName');
    //data.addColumn('number', 'PaymentAmount');

    //for (var i = 0; i < dataValues.length; i++) {
    //    data.addRow([dataValues[i].PlanName, dataValues[i].PaymentAmount]);
    //}

      var data = google.visualization.arrayToDataTable([
        ["Element", "Density", { role: "style" } ],
        ["Copper", 8.94, "#b87333"],
        ["Silver", 10.49, "silver"],
        ["Gold", 19.30, "gold"],
        ["Platinum", 21.45, "color: #e5e4e2"]
      ]);

      var view = new google.visualization.DataView(data);
      view.setColumns([0, 1,
                       { calc: "stringify",
                         sourceColumn: 1,
                         type: "string",
                         role: "annotation" },
                       2]);

      var options = {
        title: "Density of Precious Metals, in g/cm^3",
        width: 600,
        height: 400,
        bar: {groupWidth: "95%"},
        legend: { position: "none" },
      };
      var chart = new google.visualization.ColumnChart(document.getElementById("columnchart_values"));
      chart.draw(view, options);
  }

