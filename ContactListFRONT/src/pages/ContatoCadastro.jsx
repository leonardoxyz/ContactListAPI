import React, { useState, useEffect } from "react";
import axios from "axios";

const apiUrl = "https://localhost:7055/api/controller"; // Verifique se a URL está correta

function ContatoCadastro() {
  const [contatos, setContatos] = useState([]);
  const [contato, setContato] = useState({ name: "", email: "", phone: "" });
  const [isEditing, setIsEditing] = useState(false);
  const [editingContactId, setEditingContactId] = useState(null);

  useEffect(() => {
    async function fetchContatos() {
      try {
        const response = await axios.get(apiUrl);
        setContatos(response.data);
      } catch (error) {
        console.error("Erro ao buscar contatos:", error);
      }
    }

    fetchContatos();
  }, []);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setContato({ ...contato, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      if (isEditing) {
        // Se estiver em modo de edição, faça uma solicitação PUT
        await axios.put(`${apiUrl}/${editingContactId}`, contato);
      } else {
        // Caso contrário, faça uma solicitação POST
        await axios.post(apiUrl, contato);
      }

      const response = await axios.get(apiUrl);
      setContatos(response.data);

      // Limpe os campos de entrada
      setContato({ name: "", email: "", phone: "" });

      setIsEditing(false);
      setEditingContactId(null);
    } catch (error) {
      console.error("Erro ao cadastrar contato:", error);
    }
  };

  const handleEdit = (id) => {
    const contactToEdit = contatos.find((c) => c.id === id);
    if (contactToEdit) {
      setContato({ ...contactToEdit });
      setIsEditing(true);
      setEditingContactId(id);
    }
  };

  const handleDelete = async (id) => {
    try {
      await axios.delete(`${apiUrl}/${id}`);
      const updatedContatos = contatos.filter((c) => c.id !== id);
      setContatos(updatedContatos);
    } catch (error) {
      console.error("Erro ao excluir contato:", error);
    }
  };

  return (
    <div className="contato-container">
      <h1>{isEditing ? "Editar Contato" : "Cadastro de Contato"}</h1>
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="Name">Nome:</label>
          <input type="text" id="Name" name="name" value={contato.name} onChange={handleInputChange} required />
        </div>
        <div className="form-group">
          <label htmlFor="Email">Email:</label>
          <input type="email" id="Email" name="email" value={contato.email} onChange={handleInputChange} required />
        </div>
        <div className="form-group">
          <label htmlFor="Phone">Telefone:</label>
          <input type="text" id="Phone" name="phone" value={contato.phone} onChange={handleInputChange} required />
        </div>
        <button type="submit" className="cta-button">
          {isEditing ? "Salvar Edição" : "Cadastrar Contato"}
        </button>
      </form>
      <h2>Lista de Contatos</h2>
      {contatos.length > 0 ? (
        <table className="contato-table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Nome</th>
              <th>Email</th>
              <th>Telefone</th>
              <th>Ações</th>
            </tr>
          </thead>
          <tbody>
            {contatos.map((c) => (
              <tr key={c.id}>
                <td>{c.id}</td>
                <td>{c.name}</td>
                <td>{c.email}</td>
                <td>{c.phone}</td>
                <td className="cta-buttons">
                  <button className="cta-edit" onClick={() => handleEdit(c.id)}>
                    Editar
                  </button>
                  <button className="cta-delete" onClick={() => handleDelete(c.id)}>
                    Excluir
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        <p>Nenhum contato encontrado.</p>
      )}
    </div>
  );
}

export default ContatoCadastro;
