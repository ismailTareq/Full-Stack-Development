export default function Over({winner, onRestart})
{
    return (
        <div id="game-over">
            <h2>Game over bitch</h2>
            {winner && <p>{winner} won!</p>}
            {!winner && <p>it's a draw fuckers</p>}
            <p>
                <button onClick={onRestart}>Rematch babyyyyy</button>
            </p>
        </div>
    )

}