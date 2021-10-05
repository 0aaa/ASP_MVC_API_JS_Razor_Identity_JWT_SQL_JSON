
$('.searchAndFiltersForms').on('submit', (event) => {

    event.preventDefault()
    
    $('#partialDiv').load('/Home/Items/'
        + `?searchBy=${encodeURIComponent($("[name = 'searchBy']").val())}`
        + `&line=${encodeURIComponent($("[name = 'Line']").val())}`
        + `&model=${encodeURIComponent($("[name = 'Model']").val())}`
        + `&frame=${encodeURIComponent($("[name = 'Frame']").val())}`
        + `&fork=${encodeURIComponent($("[name = 'Fork']").val())}`
        + `&shifter=${encodeURIComponent($("[name = 'Shifter']").val())}`
        + `&brake=${encodeURIComponent($("[name = 'Brake']").val())}`
        + `&cost=${$("[name = 'Cost']").val()}`)
})



$('.orderByLinks').on('click', (event) => {

    event.preventDefault()
    
    $('#partialDiv').load('/Home/Items/'
        + `?orderBy=${event.target.innerText}`
        + `&searchBy=${encodeURIComponent($("[name = 'searchBy']").val())}`
        + `&line=${encodeURIComponent($("[name = 'Line']").val())}`
        + `&model=${encodeURIComponent($("[name = 'Model']").val())}`
        + `&frame=${encodeURIComponent($("[name = 'Frame']").val())}`
        + `&fork=${encodeURIComponent($("[name = 'Fork']").val())}`
        + `&shifter=${encodeURIComponent($("[name = 'Shifter']").val())}`
        + `&brake=${encodeURIComponent($("[name = 'Brake']").val())}`
        + `&cost=${$("[name = 'Cost']").val()}`)
})



$('.paginationLinks').on('click', (event) => {

    event.preventDefault()
    
    $('#partialDiv').load('/Home/Items/'
        + `?itemsCurrentPage=${event.target.innerHTML}`
        + `&searchBy=${encodeURIComponent($("[name = 'searchBy']").val())}`
        + `&line=${encodeURIComponent($("[name = 'Line']").val())}`
        + `&model=${encodeURIComponent($("[name = 'Model']").val())}`
        + `&frame=${encodeURIComponent($("[name = 'Frame']").val())}`
        + `&fork=${encodeURIComponent($("[name = 'Fork']").val())}`
        + `&shifter=${encodeURIComponent($("[name = 'Shifter']").val())}`
        + `&brake=${encodeURIComponent($("[name = 'Brake']").val())}`
        + `&cost=${$("[name = 'Cost']").val()}`)
})