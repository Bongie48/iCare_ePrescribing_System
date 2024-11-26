
'use strict'

function $(selector) {
    return document.querySelector(selector)
}

function find(el, selector) {
    let finded
    return (finded = el.querySelector(selector)) ? finded : null
}

function siblings(el) {
    const siblings = []
    for (let sibling of el.parentNode.children) {
        if (sibling !== el) {
            siblings.push(sibling)
        }
    }
    return siblings
}

const showAsideBtn = $('.show-side-btn')
const sidebar = $('.sidebar')
const wrapper = $('#wrapper')

showAsideBtn.addEventListener('click', function () {
    $(`#${this.dataset.show}`).classList.toggle('show-sidebar')
    wrapper.classList.toggle('fullwidth')
})

if (window.innerWidth < 767) {
    sidebar.classList.add('show-sidebar');
}

window.addEventListener('resize', function () {
    if (window.innerWidth > 767) {
        sidebar.classList.remove('show-sidebar')
    }
})

// dropdown menu in the side nav
var slideNavDropdown = $('.sidebar-dropdown');

$('.sidebar .categories').addEventListener('click', function (event) {
    event.preventDefault()

    const item = event.target.closest('.has-dropdown')

    if (!item) {
        return
    }

    item.classList.toggle('opened')

    siblings(item).forEach(sibling => {
        sibling.classList.remove('opened')
    })

    if (item.classList.contains('opened')) {
        const toOpen = find(item, '.sidebar-dropdown')

        if (toOpen) {
            toOpen.classList.add('active')
        }

        siblings(item).forEach(sibling => {
            const toClose = find(sibling, '.sidebar-dropdown')

            if (toClose) {
                toClose.classList.remove('active')
            }
        })
    } else {
        find(item, '.sidebar-dropdown').classList.toggle('active')
    }
})

$('.sidebar .close-aside').addEventListener('click', function () {
    $(`#${this.dataset.close}`).classList.add('show-sidebar')
    wrapper.classList.remove('margin')
})


// Global defaults
Chart.defaults.global.animation.duration = 2000; // Animation duration
Chart.defaults.global.title.display = false; // Remove title
Chart.defaults.global.defaultFontColor = '#71748c'; // Font color
Chart.defaults.global.defaultFontSize = 13; // Font size for every label

// Tooltip global resets
Chart.defaults.global.tooltips.backgroundColor = '#111827'
Chart.defaults.global.tooltips.borderColor = 'blue'

// Gridlines global resets
Chart.defaults.scale.gridLines.zeroLineColor = '#3b3d56'
Chart.defaults.scale.gridLines.color = '#3b3d56'
Chart.defaults.scale.gridLines.drawBorder = false

// Legend global resets
Chart.defaults.global.legend.labels.padding = 0;
Chart.defaults.global.legend.display = false;

// Ticks global resets
Chart.defaults.scale.ticks.fontSize = 12
Chart.defaults.scale.ticks.fontColor = '#71748c'
Chart.defaults.scale.ticks.beginAtZero = false
Chart.defaults.scale.ticks.padding = 10

// Elements globals
Chart.defaults.global.elements.point.radius = 0

// Responsivess
Chart.defaults.global.responsive = true
Chart.defaults.global.maintainAspectRatio = false



var chart = document.getElementById('chart3');
var myChart = new Chart(chart, {
    type: 'line',
    data: {
        labels: ["One", "Two", "Three", "Four", "Five", 'Six', "Seven", "Eight"],
        datasets: [{
            label: "Lost",
            lineTension: 0.2,
            borderColor: '#d9534f',
            borderWidth: 1.5,
            showLine: true,
            data: [3, 30, 16, 30, 16, 36, 21, 40, 20, 30],
            backgroundColor: 'transparent'
        }, {
            label: "Lost",
            lineTension: 0.2,
            borderColor: '#5cb85c',
            borderWidth: 1.5,
            data: [6, 20, 5, 20, 5, 25, 9, 18, 20, 15],
            backgroundColor: 'transparent'
        },
        {
            label: "Lost",
            lineTension: 0.2,
            borderColor: '#f0ad4e',
            borderWidth: 1.5,
            data: [12, 20, 15, 20, 5, 35, 10, 15, 35, 25],
            backgroundColor: 'transparent'
        },
        {
            label: "Lost",
            lineTension: 0.2,
            borderColor: '#337ab7',
            borderWidth: 1.5,
            data: [16, 25, 10, 25, 10, 30, 14, 23, 14, 29],
            backgroundColor: 'transparent'
        }]
    },
    options: {
        scales: {
            yAxes: [{
                gridLines: {
                    drawBorder: false
                },
                ticks: {
                    stepSize: 12
                }
            }],
            xAxes: [{
                gridLines: {
                    display: false,
                },
            }],
        }
    }
})
function myFunction() {
    var popup = document.getElementById("myPopup");
    popup.classList.toggle("show");
}
document.addEventListener("DOMContentLoaded", function () {
    const prescriptionToggle = document.querySelector('.toggle-prescription');
    const stockToggle = document.querySelector('.toggle-stock');
    const reportToggle = document.querySelector('.toggle-report');

    const prescriptionSubMenu = prescriptionToggle.nextElementSibling;
    const stockSubMenu = stockToggle.nextElementSibling;
    const reportSubMenu = reportToggle.nextElementSibling;

    // Toggle Prescription Sub-menu
    prescriptionToggle.addEventListener('click', function (e) {
        e.preventDefault();
        prescriptionSubMenu.style.display = (prescriptionSubMenu.style.display === 'block') ? 'none' : 'block';
        prescriptionToggle.classList.toggle('active');
        stockSubMenu.style.display = 'none'; // Close other sub-menus if opened
        reportSubMenu.style.display = 'none';
    });

    // Toggle Stock Sub-menu
    stockToggle.addEventListener('click', function (e) {
        e.preventDefault();
        stockSubMenu.style.display = (stockSubMenu.style.display === 'block') ? 'none' : 'block';
        stockToggle.classList.toggle('active');
        prescriptionSubMenu.style.display = 'none'; // Close other sub-menus if opened
        reportSubMenu.style.display = 'none';
    });

    // Toggle Report Sub-menu
    reportToggle.addEventListener('click', function (e) {
        e.preventDefault();
        reportSubMenu.style.display = (reportSubMenu.style.display === 'block') ? 'none' : 'block';
        reportToggle.classList.toggle('active');
        prescriptionSubMenu.style.display = 'none'; // Close other sub-menus if opened
        stockSubMenu.style.display = 'none';
    });

    // Highlight active sub-item on click
    const subItems = document.querySelectorAll('.sub-item a');
    subItems.forEach(item => {
        item.addEventListener('click', function () {
            subItems.forEach(i => i.parentNode.classList.remove('active'));
            this.parentNode.classList.add('active');
        });
    });
});

