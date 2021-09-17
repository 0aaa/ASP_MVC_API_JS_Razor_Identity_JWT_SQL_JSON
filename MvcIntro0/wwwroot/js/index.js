const authTkn = "auth_token"



async function getItems() {

    const fetchResult = await fetch('/api/bikes', {
        method: 'GET',
        headers: {
            'Authorization': 'bearer ' + sessionStorage.getItem(authTkn)
        }
    })

    if (fetchResult.ok == true) {

        const items = await fetchResult.json()

        let table = document.querySelector('tbody')

        items.forEach(item => table.append(addRow(item)))
    }
}



function addRow(bike) {

    const row = document.createElement('tr')
    row.id = bike.bikeId

    const tableData = []
    let j = 0


    for (var i = 0; i < 7; i++) {

        tableData[i] = document.createElement('td')
    }
    tableData.push(document.createElement('button'))
    tableData.push(document.createElement('button'))

    tableData[j++].append(bike.line)
    tableData[j++].append(bike.model)
    tableData[j++].append(bike.frame)
    tableData[j++].append(bike.fork)
    tableData[j++].append(bike.shifter)
    tableData[j++].append(bike.brake)
    tableData[j++].append(bike.cost)

    tableData[j].innerText = 'Set from form'
    tableData[j].className = 'btn border m-1'
    tableData[j++].addEventListener('click', async (event) => {

        event.preventDefault()

        const postPut = document.forms['postPutForm']

        editItem(bike.bikeId,
            postPut.elements['line'].value,
            postPut.elements['model'].value,
            postPut.elements['frame'].value,
            postPut.elements['fork'].value,
            postPut.elements['shifter'].value,
            postPut.elements['brake'].value,
            postPut.elements['cost'].value
        )
    })

    tableData[j].innerText = 'Delete'
    tableData[j].className = 'btn border m-1'
    tableData[j].addEventListener('click', async (event) => {

        event.preventDefault()

        deleteItem(bike.bikeId)
    })


    for (var i = 0; i < tableData.length; i++) {

        row.append(tableData[i])
    }


    return row
}



async function addItem(line, model, frame, fork, shifter, brake, cost) {

    const fetchResult = await fetch('/api/bikes', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + sessionStorage.getItem(authTkn)
        },
        body: JSON.stringify({
            line,
            model,
            frame,
            fork,
            shifter,
            brake,
            cost: parseInt(cost)
        })
    })

    if (fetchResult.ok == true) {

        document.getElementById('alertMessage').style.display = 'none'

        const bike = await fetchResult.json()

        document.querySelector('tbody').append(addRow(bike))
    }
    else {

        errorsHandling(fetchResult)
    }
}



async function editItem(bikeId, line, model, frame, fork, shifter, brake, cost) {

    line = line == '' ? document.getElementById(bikeId).children[0].innerText : line
    model = model == '' ? document.getElementById(bikeId).children[1].innerText : model
    frame = frame == '' ? document.getElementById(bikeId).children[2].innerText : frame
    fork = fork == '' ? document.getElementById(bikeId).children[3].innerText : fork
    shifter = shifter == '' ? document.getElementById(bikeId).children[4].innerText : shifter
    brake = brake == '' ? document.getElementById(bikeId).children[5].innerText : brake
    cost = cost == 0 ? document.getElementById(bikeId).children[6].innerText : cost


    const fetchResult = await fetch(`/api/bikes`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + sessionStorage.getItem(authTkn)
        },
        body: JSON.stringify({
            bikeId,
            line,
            model,
            frame,
            fork,
            shifter,
            brake,
            cost: parseInt(cost)
        })
    })

    if (fetchResult.ok == true) {

        document.getElementById(bikeId).replaceWith(addRow({ bikeId, line, model, frame, fork, shifter, brake, cost }))

        console.log(`${bikeId} edited`)
    } else {

        errorsHandling(fetchResult)
    }
}



async function deleteItem(bikeId) {

    const fetchResult = await fetch(`/api/bikes/${bikeId}`, {
        method: 'DELETE',
        headers: {
            'Authorization': 'bearer ' + sessionStorage.getItem(authTkn)
        }
    })
    if (fetchResult.ok == true) {

        document.getElementById(bikeId).remove()

        console.log(`${bikeId} deleted`)
    }
}



async function errorsHandling(fetchResult) {

    document.getElementById('alertMessage').innerHTML = ''

    const err = await fetchResult.json()

    if (err) {

        if (err.errors) {

            for (var e in err.errors) {

                errorsPrint(err.errors[e])
            }
        }
    }

    document.getElementById('alertMessage').style.display = 'block'
}



document.forms['postPutForm'].addEventListener('submit', (event) => {

    event.preventDefault()

    const postPut = document.forms['postPutForm']

    addItem(postPut.elements['line'].value,
        postPut.elements['model'].value,
        postPut.elements['frame'].value,
        postPut.elements['fork'].value,
        postPut.elements['shifter'].value,
        postPut.elements['brake'].value,
        postPut.elements['cost'].value
    )
})



function errorsPrint(err) {

    for (var e in err) {

        const message = document.createElement('p')

        message.append(err[e])

        document.getElementById('alertMessage').append(message)
    }
}



async function getTokenAsync() {

    const userInput = {
        name: document.getElementById('name').value,
        password: document.getElementById('password').value
    }


    const fetchResult = await fetch('api/user/token', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userInput)
    })


    const data = await fetchResult.json()


    if (fetchResult.ok == true) {

        sessionStorage.setItem(authTkn, data.access_token)


        return true
    } else {

        sessionStorage.removeItem(authTkn)

        alert(`Error: ${fetchResult.status}`)


        return false
    }
}



document.getElementById('signingInForm').addEventListener('submit', async (event) => {

    event.preventDefault()

    if (await getTokenAsync() == false) {

        location.reload()
    }

    getItems()
})