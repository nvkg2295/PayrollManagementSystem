async function loadEmployees() {

    try {
        const response = await fetch('/api/employees');

        if (!response.ok) {
            alert('Unable to load employees');
            return;
        }

        const employees = await response.json();
        const tbody = document.getElementById('employeesBody');

        tbody.innerHTML = '';

        employees.forEach(function (emp) {
            var row = '<tr>' +
                '<td>' + emp.employeeId + '</td>' +
                '<td>' + emp.employeeName + '</td>' +
                '<td>' + emp.departmentName + '</td>' +
                '<td>' + emp.basicSalary + '</td>' +
                '</tr>';

            tbody.insertAdjacentHTML('beforeend', row);
        });

        // Show the employee table container after loading data
        var container = document.getElementById('employeeTableContainer');
        if (container) container.style.display = 'block';
    }
    catch (error) {
        console.error(error);
        alert('Error loading employees');
    }
}

async function runPayroll() {
    // Support both the new month input (#payrollMonth) and the legacy separate
    // month/year controls. Be defensive if elements are missing (null).
    let month, year;
    const MIN_YEAR = 1900;
    const MAX_YEAR = 2100;

    const payrollMonthEl = document.getElementById('payrollMonth');
    const payrollValue = payrollMonthEl ? payrollMonthEl.value : '';

    if (payrollValue) {
        const parts = payrollValue.split('-');
        if (parts.length !== 2) {
            alert('Invalid month/year format.');
            if (payrollMonthEl) payrollMonthEl.focus();
            return;
        }

        year = parseInt(parts[0], 10);
        month = parseInt(parts[1], 10);

        if (isNaN(year) || year < MIN_YEAR || year > MAX_YEAR || String(parts[0]).length !== 4) {
            alert('Please select a valid 4-digit year between ' + MIN_YEAR + ' and ' + MAX_YEAR + '.');
            if (payrollMonthEl) payrollMonthEl.focus();
            return;
        }
    }
    else {
        // Fallback to legacy inputs (#month and #year)
        const monthEl = document.getElementById('month');
        const yearEl = document.getElementById('year');

        if (!monthEl || !yearEl) {
            alert('Month input not found.');
            return;
        }
    }

    try {

        const response = await fetch('/api/payroll/run', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                month: month,
                year: year
            })
        });

        if (!response.ok) {

            const errorMessage =
                await response.text();

            alert(errorMessage);

            return;
        }

        const result =
            await response.json();

        alert(result.message || 'Payroll generated successfully');

        loadPayroll(month, year);
    }
    catch (error) {

        console.error(error);

        alert('Unexpected error occurred');
    }
}

function loadPayrollFromInputs() {
    // Support new month input and legacy inputs. Defensive when elements missing.
    const payrollMonthEl = document.getElementById('payrollMonth');
    const payrollValue = payrollMonthEl ? payrollMonthEl.value : '';
    const MIN_YEAR = 1900;
    const MAX_YEAR = 2100;

    let month, year;

    if (payrollValue) {
        const parts = payrollValue.split('-');
        if (parts.length !== 2) {
            alert('Invalid month/year format.');
            return;
        }

        year = parseInt(parts[0], 10);
        month = parseInt(parts[1], 10);

        if (isNaN(year) || year < MIN_YEAR || year > MAX_YEAR || String(parts[0]).length !== 4) {
            alert('Please enter a valid 4-digit year between ' + MIN_YEAR + ' and ' + MAX_YEAR + '.');
            if (payrollMonthEl) payrollMonthEl.focus();
            return;
        }
    }
    else {
        const monthEl = document.getElementById('month');
        const yearEl = document.getElementById('year');

        if (!monthEl || !yearEl) {
            alert('Month/year inputs not found.');
            return;
        }
    }

    loadPayroll(month, year);
}

async function loadPayroll(month, year) {
    try {
        // Hide existing results and clear table while new data is loading
        var resultsContainer = document.getElementById('payrollResultsContainer');
        var tbody = document.getElementById('payrollBody');
        if (resultsContainer) resultsContainer.style.display = 'none';
        if (tbody) tbody.innerHTML = '';

        const response = await fetch('/api/payroll/' + month + '/' + year);

        if (!response.ok) {
            alert('Payroll not found');
            return;
        }

        const payroll = await response.json();

        payroll.forEach(function (item) {
            var row = '<tr>' +
                '<td>' + item.employeeId + '</td>' +
                '<td>' + item.employeeName + '</td>' +
                '<td>' + item.basicSalary + '</td>' +
                '<td>' + item.workingDays + '</td>' +
                '<td>' + item.daysPresent + '</td>' +
                '<td>' + item.grossPay + '</td>' +
                '<td>' + item.pfDeduction + '</td>' +
                '<td>' + item.professionalTax + '</td>' +
                '<td>' + item.netPay + '</td>' +
                '<td>' +
                '<button class="btn btn-primary btn-sm" onclick="viewPayslip(' + item.payrollRunId + ',' + item.employeeId + ')">Print Payslip</button>' +
                '</td>' +
                '</tr>';

            tbody.insertAdjacentHTML('beforeend', row);
        });

        // Show the payroll results container after loading data
        if (resultsContainer) resultsContainer.style.display = 'block';
    }
    catch (error) {
        console.error(error);
        alert('Error loading payroll');
    }
}

async function viewPayslip(PayrollRunId, employeeId) {
    try {
        const response = await fetch('/api/payroll/' + PayrollRunId + '/slip/' + employeeId);

        if (!response.ok) {
            alert('Payslip not found');
            return;
        }

        var p = await response.json();
        var win = window.open('', '_blank');

        var payslipHtml = '' +
            '<!doctype html>' +
            '<html>' +
            '<head>' +
            '<meta charset="utf-8">' +
            '<title>Payslip</title>' +
            '<style>' +
            'body{font-family:Arial;padding:20px;}' +
            'table{width:100%;border-collapse:collapse;}' +
            'td{border:1px solid #000;padding:10px;}' +
            '</style>' +
            '</head>' +
            '<body>' +
            '<h2>Payslip</h2>' +
            '<table>' +
            '<tr><td>Employee</td><td>' + (p.employeeName || '') + '</td></tr>' +
            '<tr><td>Month</td><td>' + (p.payrollMonth || '') + '</td></tr>' +
            '<tr><td>Year</td><td>' + (p.payrollYear || '') + '</td></tr>' +
            '<tr><td>Basic Salary</td><td>' + (p.basicSalary || '') + '</td></tr>' +
            '<tr><td>Gross Pay</td><td>' + (p.grossPay || '') + '</td></tr>' +
            '<tr><td>PF Deduction</td><td>' + (p.pfDeduction || '') + '</td></tr>' +
            '<tr><td>Professional Tax</td><td>' + (p.professionalTax || '') + '</td></tr>' +
            '<tr><td>Net Pay</td><td>' + (p.netPay || '') + '</td></tr>' +
            '</table>' +
            '<br/>' +
            '<button onclick="window.print()">Print</button>' +
            '</body>' +
            '</html>';

        win.document.write(payslipHtml);
        win.document.close();
    }
    catch (error) {
        console.error(error);
        alert('Error loading payslip');
    }
}
