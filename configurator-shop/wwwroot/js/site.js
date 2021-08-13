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

//Cart popover while adding new item to the cart
jQuery(document).ready(function($) {
    var cartBtn = document.getElementById('cartButton');
    var cartPopover = new bootstrap.Popover(cartBtn);
    
    var button = $('.btn');
    button.each(function(el) {
        (this.innerText || this.textContent) === 'Купить' ? this.classList.add('buy-button') : null; 
    });
    
    $('.buy-button').click(async function() {
        cartPopover.show();
        await sleep(2000);
        cartPopover.hide();
    });
});

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

    $('#saveProfile').click(function() {
        toggleState();
    });
});

