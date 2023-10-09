import React, { useState } from 'react';
import axios from 'axios';

function ContatoCadastro() {
  const [contato, setContato] = useState({ Name: '', Email: '', Cpf: 0 });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setContato({ ...contato, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post('https://localhost:7055/api/controller/', contato);
      console.log('Contato cadastrado com sucesso:', response.data);
    } catch (error) {
      console.error('Erro ao cadastrar contato:', error);
    }
  };

  return (
    <div>
      <h1>Cadastro de Contato</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="Name">Nome:</label>
          <input type="text" id="Name" name="Name" value={contato.Name} onChange={handleChange} />
        </div>
        <div>
          <label htmlFor="Email">Email:</label>
          <input type="email" id="Email" name="Email" value={contato.Email} onChange={handleChange} />
        </div>
        <div>
          <label htmlFor="Cpf">CPF:</label>
          <input type="text" id="Cpf" name="Cpf" value={contato.Cpf} onChange={handleChange} />
        </div>
        <button type="submit">Cadastrar Contato</button>
      </form>
    </div>
  );
}

export default ContatoCadastro;
