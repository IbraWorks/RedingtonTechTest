import { useState, useEffect } from 'react'
import axios from 'axios';
import './App.css'

function App() {
  const [firstValue, setfirstValue] = useState(0);
  const [secondValue, setsecondValue] = useState(0);
  const [calculationType, setcalculationType] = useState(0);
  const [result, setResult] = useState(null);
  const [errors, setErrors] = useState([]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post('http://localhost:5138/api/probabilities', {
        firstValue,
        secondValue,
        calculationType: calculationType,
      });

      setResult(response.data);
      setErrors([]);
    } catch (error) {
      if (error.response && error.response.data && error.response.data.errors) {
        const apiErrors = error.response.data.errors;
        const errorMessages = Object.values(apiErrors).flat();
        setErrors(errorMessages);
      } else {
        console.error('Error:', error);
        // we would handle other error cases here
      }
    }
  };

  useEffect(() => {
    console.log("results changed: ", result);
  }, [result, errors]);

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <label>
          Probability A:
          <input
            type="number"
            step="0.01"
            value={firstValue}
            onChange={(e) => setfirstValue(parseFloat(e.target.value))}
            required
          />
        </label>
        <br />
        <label>
          Probability B:
          <input
            type="number"
            step="0.01"
            value={secondValue}
            onChange={(e) => setsecondValue(parseFloat(e.target.value))}
            required
          />
        </label>
        <br />
        <label>
          Calculating Function:
          <input
            type="number"
            step="1"
            value={calculationType}
            onChange={(e) => setcalculationType(parseFloat(e.target.value))}
            required
          />
        </label>
        <br />
        <button type="submit">Send</button>
      </form>

      {errors.length > 0 && (
        <div>
          <h2>Validation Errors:</h2>
          <ul>
            {errors.map((error, index) => (
              <li key={index}>{error}</li>
            ))}
          </ul>
        </div>
      )}

      {result !== null && (
        <div>
          <h2>Result:</h2>
          <p>{result}</p>
        </div>
      )}
    </div>
  );
}

export default App
