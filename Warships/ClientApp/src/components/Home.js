import React, { Component } from 'react';


export class Home extends Component {
    static displayName = Home.name;


    constructor(props) {
        super(props);
        this.state = { move: "a", loading: true };
    }

    componentDidMount() {
        this.GetMove();
    }

    static renderMove(move) {
        return (
            <div>
                <h2>{move}</h2>

            </div>
        );
    }


    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderMove(this.state.move);

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
                    <button onClick={NPCvsNPC}>Computer vs Computer</button>
                    <button onClick={PvsNPC}>Human vs Computer</button>
                    <button onClick={PvP}>Human vs Human</button>

                </div>

                {contents}
            </div>
        );

       
    }



    async GetMove() {

        const response = await fetch('Player');
        const data = await response.json();

        this.setState({ move: data, loading: false });
    }

 /*   
*/

}

function NPCvsNPC(event, humans) {
}
function PvsNPC(event, humans) {

}
function PvP(event, humans) {

}
