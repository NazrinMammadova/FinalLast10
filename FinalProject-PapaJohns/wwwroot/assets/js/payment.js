function formatCardNumber(input) {
    var value = input.value.replace(/\D/g, '');

    if (value.length > 16) {
        value = value.slice(0, 16);
    }

    var formattedValue = '';
    for (var i = 0; i < value.length; i++) {
        if (i > 0 && i % 4 === 0) {
            formattedValue += '-';
        }
        formattedValue += value.charAt(i);
    }

    input.value = formattedValue;
}


