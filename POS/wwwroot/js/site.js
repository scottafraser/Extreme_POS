

function book_suggestion()
{
var food = document.getElementById("food").value;
var xhr;
 if (window.XMLHttpRequest) { // Mozilla, Safari, ...
    xhr = new XMLHttpRequest();
} else if (window.ActiveXObject) { // IE 8 and older
    xhr = new ActiveXObject("Microsoft.XMLHTTP");
}
var data = "name=" + book;
     xhr.open("POST", "book-suggestion.php", true); 
     xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");                  
     xhr.send(data);
}