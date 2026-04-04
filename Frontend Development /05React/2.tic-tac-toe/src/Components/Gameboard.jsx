


export default function Gameboard({onSelectedsquare,board}){
    
    // const [gameboard,setGameboard] = useState(initialGameboard)

    // function handleSelectedsquare(row,column){
    //     setGameboard((prevGameBoard) => {
    //         const updatedBoard = [...prevGameBoard.map(innerarray => [...innerarray])]
    //         updatedBoard[row][column] = activeplayerSymbol
    //         return updatedBoard
    //     })
    //     onSelectedsquare()
    // }

    return(
        <ol id="game-board">
            {board.map((row,rowIndex) => (
                <li key={rowIndex}>
                    <ol>
                        {
                            row.map((playerSymbol,colIndex) => (
                                <li key={colIndex}>
                                    <button onClick={() => onSelectedsquare(rowIndex,colIndex)} disabled={playerSymbol !== null}>{playerSymbol}  </button>
                                </li>
                            ))
                        }
                    </ol>
                </li>
            ))}
        </ol>
    )
}

