//Orders profile table animation and hyperlinks
jQuery(document).ready(function($) {
    obj = $(".clickable-row");
    obj.css("transition", "all 250ms");
    obj.click(function() {
        window.location = $(this).data("href");
    });
    obj.mouseover(function() {
        $(this).css("cursor", "pointer");
        $(this).css("border-left", "10px solid");
    });
    obj.mouseout(function() {
        $(this).css("border-left", "5px solid");
    });
});

//Sleep function
function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

//Dynamic main to header offset
jQuery(document).ready(async function($) {
    await $('main').css('margin-top', $('header')[0].offsetHeight) 
});

//Cart cookies | Using js-cookie open-source library
async function addToCart(id) {
    // Cookies.remove('Cart'); -- Debug string
    
    let cartContent = Cookies.get('Cart');
    if (cartContent !== undefined) {
        cartContent = JSON.parse(cartContent);
    } else {
        cartContent = [];
    }
    if (cartContent.some(item => item.id === id)) {
        cartContent.filter(item => item.id === id)[0].amount++;
    } else {
        cartContent.push({
            "id": id,
            "amount": 1
        });
    }
    Cookies.set('Cart', JSON.stringify(cartContent), {expires: 1}); //expiration time set to 24hrs
}

//Cart cookies | Using js-cookie open-source library
async function changeCart(id, value) {
    // Cookies.remove('Cart'); -- Debug string
    if (value == null) {
        value = 0;
    }
    let cartContent = Cookies.get('Cart');
    if (cartContent !== undefined) {
        cartContent = JSON.parse(cartContent);
    } else {
        cartContent = [];
    }
    var total = $('#total-price');
    if (cartContent.some(item => item.id === id)) {
        var found = cartContent.filter(item => item.id === id)[0];
        total.html(Number(total.html()) + Number($('span#'+id).html()) * (value - Number(cartContent.filter(item => item.id === id)[0].amount)));
        if (value === 0){
            cartContent.splice($.inArray(found, cartContent), 1);
        } else {
            cartContent.filter(item => item.id === id)[0].amount = value;
        }
    } else {
        cartContent.push({
            "id": id,
            "amount": value
        });
        total.html(Number(total.html()) + Number($('span#'+id).html()) * value);
    }
    
    Cookies.set('Cart', JSON.stringify(cartContent), {expires: 1}); //expiration time set to 24hrs
}

//Cart popover while adding new item to the cart
jQuery(document).ready(function($) {
    var cartBtn = document.getElementById('cartButton');
    var cartPopover = new bootstrap.Popover(cartBtn);
    
    var button = $('.btn');
    button.each(function(el) {
        (this.innerText || this.textContent) === 'Купить' ? this.classList.add('buy-button') : null; 
    });
    
    $('.amount-input').change(async function() {
        await changeCart(this.id, Number($(this).val()));
    });
    
    $('.buy-button').click(async function() {
        await addToCart(this.id);
        cartPopover.show();
        await sleep(2000);
        cartPopover.hide();
    });
});

//Cart items counter
function cartItemsCount() {
    let result = undefined;
    let content = Cookies.get('Cart');
    if (content !== undefined) {
        result = 0;
        content = JSON.parse(content);
        for (let item in content) {
            result += content[item].amount;
        }
    }
    
    return result;
}

//Cart badge displayer
function displayBadge() {
    let totalItems = cartItemsCount();
    let badge = $('#cartBadge');
    if (totalItems !== undefined) {
        badge.each(function() {
            this.innerText = totalItems;
            this.toggleAttribute('hidden', false);
        });
    } else {
        badge.each(function() {this.toggleAttribute('hidden', true)});
    }
}

//Comparison of Cart cookie over a very small amount of time
function checkCartCookie() {
    let oldCookie = Cookies.get('Cart');
    
    return function() {
        let newCookie = Cookies.get('Cart');
        if (oldCookie !== newCookie) {
            displayBadge();
            oldCookie = newCookie;
        }
    }
}

//Display badge on startup or reload
jQuery(document).ready(function($) {displayBadge()});

//Check for changing of Cart cookie every 100ms
window.setInterval(checkCartCookie(), 100);

//Profile fields state toggler
jQuery(document).ready(function($) {
    var toggleState = function() {
        $('#saveProfile').each(function() {this.toggleAttribute('hidden');});
        $('#changeProfile').each(function() {this.toggleAttribute('hidden');});
        $('.profileInp').each(function() {this.toggleAttribute('disabled');});
    }
    
   $('#changeProfile').click(function() {
        toggleState();
   });
});

//Search
function searchFunction(input) {
    let filter = input.value.toUpperCase();
    let items = $('.searchItem');
    items.each(function() {
        let title = this.querySelector('.searchItemTitle').innerText;
        if (title.toUpperCase().indexOf(filter) > -1) {
            this.toggleAttribute('hidden', false);
        }
        else {
            this.toggleAttribute('hidden', true);
        }
    });
}

//Smoother version of active selectors
function checkSelect() {
    return function() {
        $('select').each(function() {
            if (this === document.activeElement) {
                this.classList.remove('rounded-pill');
                this.classList.add('active-select');
            } else {
                this.classList.add('rounded-pill');
                this.classList.remove('active-select');
            }
        });
    }
}

//Helper checking selector state over time
window.setInterval(checkSelect(), 100);



