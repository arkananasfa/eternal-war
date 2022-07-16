const uuid = require('uuid').v4
const express = require('express')
const socketIO = require('socket.io')
const path = require('path')

const PORT = process.env.PORT || 3000;
const INDEX = '/index.html';

const app = express()

app.get('/download', (req, res) => {
    res.download(path.join(__dirname, 'files', 'programm1.rar'))
})

app.get('/download1', (req, res) => {
    res.sendFile('/files/programm1.rar', { root: __dirname })
})

const server = app
  .use((req, res) => res.sendFile(INDEX, { root: __dirname }))
  .listen(PORT, () => console.log(`Listening on ${PORT}`));

const io = socketIO(server, {
    transports: ['websocket']
})

io.on('connection', (socket) => {
    let roomName = 'R'
    socket.emit('fs:connected', {code : socket.id})
    console.log('connected; new client id: ' + socket.id)

    socket.join('waiting room')
    const waitingRoom = io.sockets.adapter.rooms['waiting room']

    const clients = waitingRoom.sockets
    const clientsNumber = Object.keys(clients).length;
    console.log('now connected to waiting room: ' + clientsNumber)

    if (waitingRoom.length==2) {
        console.log('2 players connected. Match is starting')
        for  (var clientID in clients) {
            roomName += clientID
        }
        let i = 1        
        console.log('game will be in room with name: ' + roomName)
        for (var clientID in clients) {
            var clientSocket = io.sockets.connected[clientID]
            clientSocket.leave('waiting room')
            clientSocket.join(roomName)
            clientSocket.emit('fs:start', { team: i, roomName: roomName})
            i++
        }
        console.log('Massages was sended to clients. Match has been started.')
    }

    socket.on('fc:getRoomName', (_roomName) => {
        roomName = _roomName.roomName
    })

    socket.on('fc:action', (action) => {
        console.log(action)
        socket.broadcast.to(roomName).emit('fs:action', action)
    })

    socket.on('disconnect', () => {
        console.log('disconnected; client id:' + socket.id)
        if (roomName == 'R') {
            socket.leave('waiting room')
            console.log('Client has kicked from waiting room')
        } else {
            console.log('Match ' + roomName + ' was ended.')
            let room = io.sockets.adapter.rooms[roomName] 
            if (room) {
                let clients = room.sockets
                reason = 'player disconnected'
                for (var clientID in clients) {
                    console.log('Client ' + socket.id + ' leave server. Reason: ' + reason)
                    let clientf = io.sockets.connected[clientID]
                    clientf.leave(roomName)
                    clientf.emit('fs:leave', { reason: reason})
                }
            }
        }
    })
})