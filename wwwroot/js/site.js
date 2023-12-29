// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var pickupDateInput = document.getElementById('PickupDate');
var pickupTimeInput = document.getElementById('PickupTime');
var dropDateInput = document.getElementById('DropDate');
var dropTimeInput = document.getElementById('DropTime');

pickupDateInput.addEventListener('change', function () {
    dropDateInput.min = this.value;
    updatePickupTimeMin();
});
dropDateInput.addEventListener('change', function () {
    updateDropTimeMin()
});

pickupTimeInput.addEventListener('change', function () {
    updateDropTimeMin();
});

function updateDropTimeMin() {

    // If pickup date and drop-off date are equal, set minimum drop-off time more than pickup time
    if (pickupDateInput.value === dropDateInput.value) {
        var pickupTime = parseTime(pickupTimeInput.value);
        var minimumDropTime = new Date(pickupTime.getTime() + (3 * 60 * 60 * 1000)); // 3 hours more than pickup time
        var formattedMinDropTime = formatTime(minimumDropTime);
        dropTimeInput.min = formattedMinDropTime;
    } else {
        dropTimeInput.min = ''; // Allow any time if pickup date and drop-off date are not equal
    }

}

function updatePickupTimeMin() {
    // Set the minimum pickup time to be 2 hours from now if the pickup date is today
    var today = new Date();
    var pickupDate = new Date(pickupDateInput.value);

    if (
        pickupDate.getDate() === today.getDate() &&
        pickupDate.getMonth() === today.getMonth() &&
        pickupDate.getFullYear() === today.getFullYear()
    ) {
        var minimumPickupTime = new Date(today.getTime() + (2 * 60 * 60 * 1000)); // 2 hours in milliseconds
        var formattedMinPickupTime = formatTime(minimumPickupTime);
        pickupTimeInput.min = formattedMinPickupTime;
    }
    else
        pickupTimeInput.min = '';
}

function formatDate(date) {
    var year = date.getFullYear();
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var day = ('0' + date.getDate()).slice(-2);
    return year + '-' + month + '-' + day;
}

function formatTime(date) {
    var hours = date.getHours();
    var minutes = ('0' + date.getMinutes()).slice(-2);
    // Handle midnight (0 hours)

    return hours + ':' + minutes;
}
function parseTime(timeString) {
    // Parse the time string 'hh:mm am/pm' into a Date object
    var timeParts = timeString.split(':');
    var hours = parseInt(timeParts[0], 10);
    var minutes = parseInt(timeParts[1], 10);

    // Convert AM/PM to 24-hour format
    if (timeString.toLowerCase().indexOf('pm') !== -1 && hours < 12) {
        hours += 12;
    }
    if (timeString.toLowerCase().indexOf('am') !== -1 && hours === 12) {
        hours = 0;
    }

    return new Date(0, 0, 0, hours, minutes, 0, 0);
}