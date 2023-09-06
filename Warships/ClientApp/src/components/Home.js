import React, { Component } from 'react';


export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>
                <h1>Ahoy!</h1>
                <p>Please state your names below</p>
                <ul>
                    <li>Admiral <input name1="player1" />
                    </li>
                    <br></br>
                    <li>Admiral <input name2="player2" />
                    </li>
                </ul>
                <div>
                    <button onClick={(e) => {
                        this.ButtonStart(e, 0);
                    }}
                    >Computer vs Computer</button>
                    <button onClick={(e) => {
                        this.ButtonStart(e, 1);
                    }}>Human vs Computer</button>
                    <button onClick={(e) => {
                        this.ButtonStart(e, 2);
                    }}>Human vs Human</button>

                </div>
            </div>
        );
    }

}


function ButtonStart(event, humans)
{
    
}
