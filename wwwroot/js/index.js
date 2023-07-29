// Create the chart
var ctx = document.getElementById('expenseChart').getContext('2d');
var expenses = @Html.Raw(Json.Serialize(Model));
var expenseLimits = @Html.Raw(Json.Serialize(TempData["Limitlist"]));
console.log(expenseLimits)
var report = "";
var graphVal = "";
var currentYear = moment().year();
var currentWeek = moment().week();
var currentMonth = moment().month();
var chart = null;
var expenseType = [];
var colorMap = new Map();
function createBarChart(expense) {
    chart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: [],
            datasets: [{
                label: 'Expense Amount',
                data: [],
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            },
            {
                label: 'Expense Limit',
                data: [],
                backgroundColor: 'rgba(192, 50, 50, 1)',

            }
            ]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            plugins: {
                annotation: {
                    annotations: []
                }
            }
        }

    });


    // Extract the required data for the chart

    var expenseAmounts = [];
    var expenseTyp = [];
    // Iterate over expenses
    for (var i = 0; i < expense.length; i++) {
        var expens = expense[i];
        var index = expenseTyp.indexOf(expens.expenseType);

        // If expense type already exists in expenseType array
        if (report == "Yearly" && moment(expens.expenseDate).year() === currentYear) {
            // If expense type already exists in expenseType array
            if (index > -1) {
                // Add the expense amount to the existing bar
                expenseAmounts[index] += expens.expenseAmount;
            } else {
                // Add a new expense type and expense amount
                expenseTyp.push(expens.expenseType);
                expenseAmounts.push(expens.expenseAmount);
            }
        }

        else if (report == "Monthly" && moment(expens.expenseDate).month() === currentMonth) {

            if (index > -1) {
                // Add the expense amount to the existing bar
                expenseAmounts[index] += expens.expenseAmount;
            } else {
                // Add a new expense type and expense amount
                expenseTyp.push(expens.expenseType);
                expenseAmounts.push(expens.expenseAmount);
            }

        }

        else if (report == "Weekly" && moment(expens.expenseDate).week() === currentWeek) {
            if (index > -1) {
                // Add the expense amount to the existing bar
                expenseAmounts[index] += expens.expenseAmount;
            } else {
                // Add a new expense type and expense amount
                expenseTyp.push(expens.expenseType);
                expenseAmounts.push(expens.expenseAmount);
            }
        }

        else if (report == "") {
            if (index > -1) {
                // Add the expense amount to the existing bar
                expenseAmounts[index] += expens.expenseAmount;
            } else {
                // Add a new expense type and expense amount
                expenseTyp.push(expens.expenseType);
                expenseAmounts.push(expens.expenseAmount);
            }
        }
    }

    // Update the chart data and labels
    chart.data.labels = expenseTyp;
    chart.data.datasets[0].data = expenseAmounts;
    expenseType = expenseTyp;

    for (var i = 0; i < expenseLimits.length; i++) {
        var limit = expenseLimits[i];
        var typeIndex = expenseType.indexOf(limit.expenseType);
        var annotation = {
            type: 'line',
            yMin: limit.limit,
            yMax: limit.limit,
            xMin: typeIndex - 0.5,
            xMax: typeIndex + 0.5,
            borderColor: 'rgba(192, 50, 50, 1)',
            borderWidth: 2,

        };
        if (limit.limit > 0) {
            chart.options.plugins.annotation.annotations.push(annotation);
        }

    }

    // Update the chart
    chart.update();
}

// Function to create and update the Dot Chart
function createDotChart(expense) {
    chart = new Chart(ctx, {
        type: 'scatter', // Change the chart type to "scatter"
        data: {
            datasets: [],
        },
        options: {
            responsive: true,
            scales: {
                x: {
                    type: 'time',
                    time: {
                        unit: 'day', // Set the desired time unit, e.g., 'day', 'month', 'year', etc.

                        displayFormats: {
                            day: 'MMM D', // Set the display format for the x-axis labels
                        }
                    },

                    position: 'bottom'
                }
            },
            plugins: {
                annotation: {
                    annotations: []
                }
            }
        }
    });




    if (expenseType) {
        for (var item in expenseType) {
            type = expenseType[item]
            var chardataset = {
                label: type,
                data: [],
                backgroundColor: [], // Provide an empty array for background colors
                pointRadius: 5, // Adjust the size of the dots
            }

            var data = [];

            expense.map(function (expens) {

                const subdata = {
                    x: moment(expens.expenseDate), // Parse the date string to a Date object
                    y: expens.expenseAmount
                }


                if (expens.expenseType == type) {
                    if (report == "Yearly" && moment(expens.expenseDate).year() === currentYear) {
                        data.push(subdata)
                    }

                    else if (report == "Monthly" && moment(expens.expenseDate).month() === currentMonth) {
                        data.push(subdata)
                    }

                    else if (report == "Weekly" && moment(expens.expenseDate).week() === currentWeek) {
                        data.push(subdata)
                    }

                    else if (report == "") {
                        data.push(subdata)
                    }


                }
            })



            var color = generateColorFromString()
            if (colorMap.get(type) != null) {
                color = colorMap.get(type)
            }

            else {
                while ([...colorMap.values()].includes(color)) {
                    color = generateColorFromString()
                }
                colorMap.set(type, color);
            }

            chardataset.data = data;
            chardataset.backgroundColor = color;

            chart.data.datasets.push(chardataset)




        }

    }

    for (var i = 0; i < expenseLimits.length; i++) {
        var limit = expenseLimits[i];
        var averageLimit = Math.round(limit.limit / limit.items);

        var annotation = {
            type: 'line',
            yMin: limit.limit / limit.items,
            yMax: limit.limit / limit.items,

            borderColor: colorMap.get(limit.expenseType),
            borderWidth: 2,

        };
        if (limit.limit > 0) {
            chart.options.plugins.annotation.annotations.push(annotation);
        }

    }

    chart.update();
}

function createPieChart(expense) {
    chart = new Chart(ctx, {
        type: 'pie', // Change the chart type to "scatter"
        data: {

            labels: [],
            datasets: [
                {
                    label: "Expense Data",
                    data: [],
                    backgroundColor: [

                    ],
                    hoverOffset: 4
                }
            ]
        },

    });


    var expenseAmounts = [];
    var expenseTyp = [];
    var expenseColor = [];
    // Iterate over expenses
    for (var i = 0; i < expense.length; i++) {
        var expens = expense[i];
        var index = expenseTyp.indexOf(expens.expenseType);

        // If expense type already exists in expenseType array
        if (report == "Yearly" && moment(expens.expenseDate).year() === currentYear) {
            // If expense type already exists in expenseType array
            if (index > -1) {
                // Add the expense amount to the existing bar
                expenseAmounts[index] += expens.expenseAmount;
            } else {
                // Add a new expense type and expense amount
                expenseTyp.push(expens.expenseType);
                expenseAmounts.push(expens.expenseAmount);
                var color = generateColorFromString()
                if (colorMap.get(expens.expenseType) != null) {
                    color = colorMap.get(expens.expenseType)
                }

                else {
                    while ([...colorMap.values()].includes(color)) {
                        color = generateColorFromString()
                    }
                    colorMap.set(expens.expenseType, color);
                }

                expenseColor.push(color);
            }
        }

        else if (report == "Monthly" && moment(expens.expenseDate).month() === currentMonth) {

            if (index > -1) {
                // Add the expense amount to the existing bar
                expenseAmounts[index] += expens.expenseAmount;
            } else {
                // Add a new expense type and expense amount
                expenseTyp.push(expens.expenseType);
                expenseAmounts.push(expens.expenseAmount);
                var color = generateColorFromString()
                if (colorMap.get(expens.expenseType) != null) {
                    color = colorMap.get(expens.expenseType)
                }

                else {
                    while ([...colorMap.values()].includes(color)) {
                        color = generateColorFromString()
                    }
                    colorMap.set(expens.expenseType, color);
                }

                expenseColor.push(color);
            }

        }

        else if (report == "Weekly" && moment(expens.expenseDate).week() === currentWeek) {
            if (index > -1) {
                // Add the expense amount to the existing bar
                expenseAmounts[index] += expens.expenseAmount;
            } else {
                // Add a new expense type and expense amount
                expenseTyp.push(expens.expenseType);
                expenseAmounts.push(expens.expenseAmount);
                var color = generateColorFromString()
                if (colorMap.get(expens.expenseType) != null) {
                    color = colorMap.get(expens.expenseType)
                }

                else {
                    while ([...colorMap.values()].includes(color)) {
                        color = generateColorFromString()
                    }
                    colorMap.set(expens.expenseType, color);
                }

                expenseColor.push(color);
            }
        }

        else if (report == "") {
            if (index > -1) {
                // Add the expense amount to the existing bar
                expenseAmounts[index] += expens.expenseAmount;
            } else {
                // Add a new expense type and expense amount
                expenseTyp.push(expens.expenseType);
                expenseAmounts.push(expens.expenseAmount);
                var color = generateColorFromString()
                if (colorMap.get(expens.expenseType) != null) {
                    color = colorMap.get(expens.expenseType)
                }

                else {
                    while ([...colorMap.values()].includes(color)) {
                        color = generateColorFromString()
                    }
                    colorMap.set(expens.expenseType, color);
                }

                expenseColor.push(color);
            }
        }
    }

    // Update the chart data and labels
    chart.data.labels = expenseTyp;
    chart.data.datasets[0].data = expenseAmounts;
    chart.data.datasets[0].backgroundColor = expenseColor;
    expenseType = expenseTyp;

    chart.update();

}

function generateColorFromString() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}


// Function to update the chart based on the selected graph type
function updateChart(graphVal) {
    if (chart) {
        chart.destroy(); // Destroy the existing chart if it exists
    }

    if (graphVal === "bar") {
        createBarChart(expenses);
    }

    else if (graphVal === "dots") {
        createDotChart(expenses);
    }

    else if (graphVal === "pie") {
        createPieChart(expenses);
    }

    else {

        createBarChart(expenses);
    }
}

// Initial chart creation
updateChart("bar");

// Bind the change event using the .on() method to handle future elements
$('#graph').on('change', function () {
    graphVal = $(this).val();
    updateChart(graphVal);
});

$('#report').on('change', function () {
    var reportVal = $(this).val();
    if (reportVal == "Yearly") {
        report = "Yearly"
        updateChart(graphVal)
    }

    else if (reportVal == "Monthly") {
        report = "Monthly"
        updateChart(graphVal)
    }

    else if (reportVal == "Weekly") {
        report = "Weekly"
        updateChart(graphVal)
    }
});



