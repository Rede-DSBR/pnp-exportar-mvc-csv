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



var checkList = document.getElementById('listD');
checkList.getElementsByClassName('anchorD')[0].onclick = function(evt) {
  if (checkList.classList.contains('visible'))
    checkList.classList.remove('visible');
  else
    checkList.classList.add('visible');
}

var checkListF = document.getElementById('listF');
checkListF.getElementsByClassName('anchorD')[0].onclick = function(evt) {
  if (checkListF.classList.contains('visible'))
    checkListF.classList.remove('visible');
  else
    checkListF.classList.add('visible');
}


var checkListA = document.getElementById('listA');
checkListA.getElementsByClassName('anchorA')[0].onclick = function(evt) {
  if (checkListA.classList.contains('visible'))
    checkListA.classList.remove('visible');
  else
    checkListA.classList.add('visible');
}


