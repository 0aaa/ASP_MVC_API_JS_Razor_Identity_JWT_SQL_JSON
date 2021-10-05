
function getItems(orderBy, pageNumber) {

    $('#partialDiv').load('/Home/Items/'
        + `?orderBy=${orderBy}`
        + `&itemsCurrentPage=${pageNumber}`
        + `&searchBy=${encodeURIComponent($("[name = 'searchBy']").val())}`

        + `&line=${encodeURIComponent($("[name = 'Line']").val())}`
        + `&model=${encodeURIComponent($("[name = 'Model']").val())}`
        + `&frame=${encodeURIComponent($("[name = 'Frame']").val())}`
        + `&fork=${encodeURIComponent($("[name = 'Fork']").val())}`
        + `&shifter=${encodeURIComponent($("[name = 'Shifter']").val())}`
        + `&brake=${encodeURIComponent($("[name = 'Brake']").val())}`
        + `&cost=${$("[name = 'Cost']").val()}`)
}





$('.orderByPaginationLinks').on('click', (event) => {

    event.preventDefault()


    if (event.target.classList.contains('page-link')) {

        getItems('Line', event.target.innerText)

    } else {

        getItems(event.target.innerText, $('.active')[0].firstElementChild.innerHTML)

    }
})



$('#nextPaginationLink').on('click', (event) => {

    event.preventDefault()

    getItems(Number($('.active')[0].firstElementChild.innerHTML) + 1)
})


$('#previousPaginationLink').on('click', (event) => {

    event.preventDefault()

    getItems(Number($('.active')[0].firstElementChild.innerHTML) - 1)
})