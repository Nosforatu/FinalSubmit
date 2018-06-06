var myChart;

// Write your JavaScript code.
$(document).ready(function () {
    if ($("#snackbar").text().length > 0) {
        ShowNotification();
    }
});

//Snack Bar
function ShowNotification() {
    var snackbar = document.getElementById("snackbar");
    snackbar.className = "show";
    setTimeout(function () { snackbar.className = snackbar.className.replace("show", ""); }, 3000);
}


// Top Ten Air pressure 
$('#btn-air-pressure').on('click', function () {
    $.getJSON("/Chart/AirPressure", function (result) {
        initGraph(result.queryName, result.values);
    });
    
});

// V8 
$('#btn-v8-air-pressure').on('click', function () {
    $.getJSON("/Chart/AirPressureByCelinder", function (result) {
        initGraph(result.queryName, result.values);
    });

});

// Draw Graph
function initGraph(queryName, values) {
    var ctx = document.getElementById("RatioChart");
    var lable = [];
    var dataSet = [];
    
    for (x = 0; x < values.length; x++) {
        lable[x] = values[x].name;
        dataSet[x] = values[x].value;
    }

    if (myChart != null) {
        myChart.destroy();
    }

    myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: lable,
            datasets: [{
                label: queryName,
                data: dataSet,
                backgroundColor: 'rgba(54, 162, 235, 1)'
                
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: true,
            legend: {
                onClick: function (e) {
                    e.stopPropagation();
                }
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}

// Get Data to Modal
$(".btn-delete-vehickle").click(function () {
    var vehicleId = $(this).data("id");
    $("#vehicle-id").val(vehicleId);
})