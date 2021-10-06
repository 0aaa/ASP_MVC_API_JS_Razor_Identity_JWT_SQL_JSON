
function getItems(pageNumber, orderBy = 'Line') {

    $('#partialDiv').load('/Home/Items/'
        + `?itemsCurrentPage=${pageNumber}`
        + `&orderBy=${orderBy}`
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

        getItems(event.target.innerText)

    } else {

        getItems($('.active')[0].firstElementChild.innerHTML, event.target.innerText)

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