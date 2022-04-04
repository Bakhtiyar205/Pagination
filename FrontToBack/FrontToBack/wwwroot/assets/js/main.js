$(document).ready(function () {
  $(".owl-carusel-store").owlCarousel({
    loop: true,
    nav: true,
    responsive: {
      0: {
        items: 1,
      },
      600: {
        items: 1,
      },
      1000: {
        items: 1,
      },
    },
  });
  $(".owl-carousel-cream").owlCarousel({
    loop: true,
    nav: true,
    responsive: {
      0: {
        items: 1,
      },
      600: {
        items: 3,
      },
      1000: {
        items: 5,
      },
    },
  });

  // Side bar Click Event
  $(".fa-plus").click(function(){
    $(this).toggleClass("fa-plus");
    $(this).toggleClass("fa-minus");
    $(this).parent().parent().next().toggleClass("d-block");
    if($(".product-category").css("height")>"232px"){
      $(".product-category").css("overflow-y","scroll");
    }else{
      $(".product-category").css("overflow-y","auto")
    }
  })
    

  $(".sort-shop").click(function(){
    $(this).parent().next().toggleClass("d-block");
  })
  $(".color-show-shop").click(function(){
    $(this).next().toggleClass("d-block");
  })

  var clock = $(".clock").FlipClock({
    clockFace: "DailyCounter",
    autoStart: false,
    callbacks: {
      stop: function () {
        $(".message").html("The clock has stopped!");
      },
    },
  });

  // set time
  clock.setTime(220880);

  // countdown mode
  clock.setCountdown(true);

  // start the clock
  clock.start();


    $(".basketform").submit(function (e) {
        e.preventDefault();
        var url = $(this).attr("action");
        var token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: url,
            method: 'post',
            data: {
                __RequestVerificationToken: token
            },
            success: function (response) {
                let basketElement = $("#basket-element").attr("data-count");

            
                if (basketElement > 0) {
                    basketElement++;
                    let tr = `<tr>
                    <td>
                        `+basketElement+`
                    </td>
                    <td>
                         `+ response.data.name +`
                    </td>
                    <td>
                        `+ response.data.count +`
                    </td>
                    <td>
                     `+ response.data.price +`
                    </td>
                </tr>`;
                    $("#basketelement").find("tbody").append(tr);
                    $("#basketelement").attr('data-count', basketElement);
                }
                else
                {
                    $("#basketelement").empty();

                    let table = `<table class="table table-striped">
                         <tbody>
                            <tr>
                                <td>
                                    `+1+`
                                </td>
                                <td>
                                    `+response.data.name+`
                                </td>
                                <td>
                                          `+ response.data.count +`
                                </td>
                                <td>
                                             `+ response.data.price +`
                                </td>
                            </tr>
                    </tbody>
                    </table>`;
                    $("#basketelement").append(table);
                    $("#basketelement").attr('data-count', 1);
                }
            }
        })
    });
});
