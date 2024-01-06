//function showDetails(bookId) {
//    var detailElement = document.getElementById("detail-" + bookId);
//    var detailBlackElement = document.getElementById("black-" + bookId);

//    detailElement.classList.toggle("d-block");
//    detailBlackElement.classList.toggle("d-block");
//  }

//  document.querySelectorAll('.detail-black').forEach(function (detailBlackElement) {
//    detailBlackElement.addEventListener('click', function () {
//      var detailId = this.id.replace('black-', ''); // 'black-' önekini kaldır
//      var detailElement = document.getElementById('detail-' + detailId);

//      detailElement.classList.toggle('d-block', false);
//      detailBlackElement.classList.toggle('d-block', false);
//    });
//  });
//  document.querySelector('body').addEventListener("click",function(){

//  });

function hesapla(selectId, priceId, originalPrice) {
    var selectedSize = document.getElementById(selectId).value;

    if (selectedSize === "kicik") {
        document.getElementById(priceId).innerText = (originalPrice / 2).toFixed(2).replace(/\.00$/, '');
    } else if (selectedSize === "buyuk") {
        document.getElementById(priceId).innerText = (originalPrice * 2).toFixed(2).replace(/\.00$/, '');
    } else {
        document.getElementById(priceId).innerText = originalPrice.toFixed(2).replace(/\.00$/, '');
    }
}
