function hesapla(selectId, priceId, originalPrice) {
    var selectedSize = document.getElementById(selectId).value;

    if (selectedSize === "kicik") {
        document.getElementById(priceId).innerText = (originalPrice / 2).toFixed(2).replace(/\.00$/, '');
    } else if (selectedSize === "buyuk") {
        document.getElementById(priceId).innerText = (originalPrice * 2).toFixed(2).replace(/\.00$/, '');
    } else {
        document.getElementById(priceId).innerText = originalPrice.toFixed(2).replace(/\.00$/, '');
    }