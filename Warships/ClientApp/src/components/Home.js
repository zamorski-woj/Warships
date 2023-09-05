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
                <button onClick={ConfirmNames}>OK</button>
            </div>
        );
    }

}


function ConfirmNames() {
    
}
