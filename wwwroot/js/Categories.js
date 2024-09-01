function toggleCreateCategoryForm() {
    var tableDiv = document.getElementById('categoryTable');
    var formDiv = document.getElementById('createCategoryForm');
    var heading = document.querySelector('h1');
    var createButton = document.querySelector('.btn-primary');

    if (tableDiv.style.display === 'none') {
        tableDiv.style.display = 'block';
        formDiv.style.display = 'none';
        heading.style.display = 'block';
        createButton.style.display = 'inline-block';
    } else {
        tableDiv.style.display = 'none';
        formDiv.style.display = 'block';
        heading.style.display = 'none';
        createButton.style.display = 'none';
    }
}
document.getElementById("createCategoryForm").onsubmit = function () {
    return validateAndSubmit();
};
// Immediate validation for the Name field
function validateName() {
    var name = document.getElementById("Name").value.trim();
    var validationMessages = document.getElementById("validationMessages");

    // Clear previous validation messages
    validationMessages.innerHTML = "";

    // Check if Name field is empty
    if (name === "") {
        validationMessages.innerHTML = "Name is required.";
    }
}
function validateAndSubmit() {
    var name = document.getElementById("Name").value.trim();
    var validationMessages = document.getElementById("validationMessages");

    // Clear previous validation messages
    validationMessages.innerHTML = "";

    // Check if fields are empty
    if (name === "") {
        // Display error message
        validationMessages.innerHTML = "Name is required.";
        return false; // Prevent form submission

    }

    // If validation passes, allow the form to submit
    return true;
}


