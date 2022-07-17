// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var show = false;
  
function showCheckboxesDimensao() {
    var checkboxes =
        document.getElementById("checkBoxesDimensao");

    if (show) {
        checkboxes.style.display = "block";
        show = false;
    } else {
        checkboxes.style.display = "none";
        show = true;
    }
}





    

    
var show = true;

function showCheckboxesFato() {
var checkboxes = 
    document.getElementById("checkBoxesFato");

if (show) {
    checkboxes.style.display = "block";
    show = false;
} else {
    checkboxes.style.display = "none";
    show = true;
}
}