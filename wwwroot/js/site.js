document.addEventListener('DOMContentLoaded', function () {
    const alerts = document.querySelectorAll('.alert-dismissible');
    alerts.forEach(function (alert) {
        setTimeout(function () {
            if (alert && alert.parentNode) {
                const bsAlert = new bootstrap.Alert(alert);
                bsAlert.close();
            }
        }, 5000);
    });
});

// Confirm delete actions
function confirmDelete(message) {
    return confirm(message || 'Are you sure you want to delete this item?');
}

// Auto-submit form on filter change
document.addEventListener('DOMContentLoaded', function () {
    const statusFilter = document.getElementById('statusFilter');
    const sortBy = document.getElementById('sortBy');

    if (statusFilter) {
        statusFilter.addEventListener('change', function () {
            this.form.submit();
        });
    }

    if (sortBy) {
        sortBy.addEventListener('change', function () {
            this.form.submit();
        });
    }
});

// Set minimum date for due date input to today
document.addEventListener('DOMContentLoaded', function () {
    const dueDateInputs = document.querySelectorAll('input[type="date"]');
    const today = new Date().toISOString().split('T')[0];

    dueDateInputs.forEach(function (input) {
        if (!input.value) {
            input.min = today;
        }
    });
});