import React, { Component } from "react";
import axios from "axios";

import "./App.css";

class App extends Component {
  state = {
    values: [],
  };

  componentDidMount() {
    axios.get("https://localhost:5001/api/Values").then((response) => {
      this.setState({
        values: response.data,
      });
    });
  }

  render() {
    return (
      <div className="App">
        <h1>Reactivities</h1>
        <ul>
          {this.state.values.map((v: any) => (
            <li key={v.id}>{v.name}</li>
          ))}
        </ul>
      </div>
    );
  }
}

export default App;
