
$(document).ready(function () {
    //For pending project data
    $.ajax({
        url: '/Admin/PendingProjectsData',
        method: 'GET',
        success: function (data) {
            createPendingProjectsChart('pendingProjectsChart', data);
        },
        error: function (xhr, status, error) {
            console.error('Error fetching pending projects data:', error);
        }
    });

    //  //For ongoing project data
    $.ajax({
        url: '/Admin/OngoingProjectsData',
        method: 'GET',
        success: function (data) {
            createOngoingProjectsChart('ongoingProjectsChart', data);
        },
        error: function (xhr, status, error) {
            console.error('Error fetching pending projects data:', error);
        }
    });

    //For complete project data
    $.ajax({
        url: '/Admin/CompletedProjectsData',
        method: 'GET',
        success: function (data) {
            createCompletedProjectsChart('completedProjectsChart', data);
        },
        error: function (xhr, status, error) {
            console.error('Error fetching pending projects data:', error);
        }
    });
});


// pending-projects-chart.js
function createPendingProjectsChart(canvasId, data) {
    // Chart creation logic for pending projects
    var labels = Object.keys(data); // Extract month names as labels
    var values = Object.values(data); // Extract project counts as values

    var ctx = document.getElementById(canvasId).getContext('2d');
    new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Pending Projects Count',
                data: values,
                borderColor: 'rgb(104, 210, 232)',
                borderWidth: 2,
                color: white ,
                fill: false
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            plugins: {
                legend: {
                    labels: {
                        color: 'white' // Set label text color to white
                    }
                }
            }

        }
    });
}

// ongoing-projects-chart.js
function createOngoingProjectsChart(canvasId, data) {
    // Chart creation logic for ongoing projects
    // Chart creation logic for pending projects
    var labels = Object.keys(data); // Extract month names as labels
    var values = Object.values(data); // Extract project counts as values

    var ctx = document.getElementById(canvasId).getContext('2d');
    new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Ongoing Projects Count',
                data: values,
                borderColor: 'rgb(144, 210, 109)',
                borderWidth: 2,
                fill: false
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

// completed-projects-chart.js
function createCompletedProjectsChart(canvasId, data) {
    // Chart creation logic for completed projects
    // Chart creation logic for pending projects
    var labels = Object.keys(data); // Extract month names as labels
    var values = Object.values(data); // Extract project counts as values

    var ctx = document.getElementById(canvasId).getContext('2d');
    new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Complete Projects Count',
                data: values,
                borderColor: 'rgb(44, 78, 128)',
                borderWidth: 2,
                fill: false
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}
