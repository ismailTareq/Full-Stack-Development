import Player from "./Components/Player"
import Gameboard from "./Components/Gameboard"
import { useState } from "react"
import Log from "./Components/Log"
import Over from "./Components/Over.jsx"
import {WINNING_COMBINATIONS} from './winning-combinations.js'

function deriveActiveplayer(gameturn){
  let currentplayer = 'X'

  if( gameturn.length > 0 && gameturn[0].player === 'X' ){
    currentplayer = 'O'
  }
  return currentplayer
}

const initialGameboard = [
    [null,null,null],
    [null,null,null],
    [null,null,null]
]

function App() {
  // const [activePlayer,setactivePlayer] = useState('X')
  const [gameTurns,setgameTurns] = useState([])
  const [Players,setPlayers] = useState({
    'X': 'Player1',
    'O': 'Player2'
  })

  const activeplayer = deriveActiveplayer(gameTurns)

  let gameboard = [...initialGameboard.map(innerarray => [...innerarray])]
  for(const turn of gameTurns)
  {
      const {square,player} = turn
      const {row,col} = square

      gameboard[row][col] = player 
  }

  let winner;

  for (const combination of WINNING_COMBINATIONS){
    const firstsquare = gameboard[combination[0].row][combination[0].column]
    const secondsquare = gameboard[combination[1].row][combination[1].column]
    const thirdsquare = gameboard[combination[2].row][combination[2].column]

    if (firstsquare && firstsquare === secondsquare && firstsquare === thirdsquare){
      winner = firstsquare;
    }
  }

  const draw = gameTurns.length === 9 && !winner

  function handlePlayersquare(row,col){
    // setactivePlayer((curActiveplayer) => curActiveplayer === 'X' ? 'O':'X')
    setgameTurns(prevTurn => {
      const currentplayer = deriveActiveplayer(prevTurn)
      const updateTurn = [{square:{row:row,col},player:currentplayer},...prevTurn]
      return updateTurn
    })
  }

  function handlerestart()
  {
    setgameTurns([]);
  }

  function handleplayerchange(symbol,newname){
    setPlayers(prevname => {
      return {
        ...prevname,
        [symbol]: newname
      }
    });
  }

  return (
    <main>
      <div id="game-container">
        <ol id="players" className="highlight-player">
          <Player initalname="player1" symbol="X" isActive={activeplayer === 'X'}/>
          <Player initalname="player2" symbol="O" isActive={activeplayer === 'O'}/>
        </ol>
        {(winner || draw) && <Over winner={winner} onRestart={handlerestart}/>}
        <Gameboard onSelectedsquare={handlePlayersquare}
          board={gameboard}
        />
      </div>
      <Log turns={gameTurns}/>
    </main>
  )
}

export default App
