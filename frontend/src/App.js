import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './App.css';
import InvitedLeads from './components/InvitedLeads';
import AcceptedLeads from './components/AcceptedLeads';

export const API_URL = "https://localhost:7163";

function App() {
  const [activeTab, setActiveTab] = useState('invited');
  const [leads, setLeads] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [updatingId, setUpdatingId] = useState(null);

  useEffect(() => {
    setLoading(true);
    setError(null);
    setLeads([]);

    const statusToFetch = activeTab === 'invited' ? 'CONVIDADO' : 'ACEITO';

    axios.get(`${API_URL}/lead?status=${statusToFetch}`)
      .then(response => {
        setLeads(response.data.dados);
        setLoading(false);
      })
      .catch(error => {
        console.error(`Erro ao buscar leads ${statusToFetch}!`, error);
        setError("Não foi possível carregar os leads.");
        setLoading(false);
      });
  }, [activeTab]);

  const handleAccept = (id) => {
    setUpdatingId(id);
    axios.put(`${API_URL}/lead/accept/${id}`)
      .then(response => {
        setLeads(prevLeads => prevLeads.filter(lead => lead.id !== id));
        setUpdatingId(null);
      })
      .catch(error => {
        console.error("Erro ao aceitar lead!", error);
        alert(`Erro ao aceitar lead: ${error.message}`);
        setUpdatingId(null);
      });
  };

  const handleDecline = (id) => {
    setUpdatingId(id);
    axios.put(`${API_URL}/lead/rejected/${id}`)
      .then(response => {
        setLeads(prevLeads => prevLeads.filter(lead => lead.id !== id));
        setUpdatingId(null);
      })
      .catch(error => {
        console.error("Erro ao recusar lead!", error);
        alert(`Erro ao recusar lead: ${error.message}`);
        setUpdatingId(null);
      });
  };

  const renderTabContent = () => {
    if (loading) return <p>Carregando leads...</p>;
    if (error) return <p>{error}</p>;
    if (activeTab === 'invited') {
      return <InvitedLeads
        leads={leads}
        updatingId={updatingId}
        onAccept={handleAccept}
        onDecline={handleDecline}
      />;
    }

    if (activeTab === 'accepted') {
      return <AcceptedLeads leads={leads} />;
    }

    return null;
  };

  return (
    <div className="App">
      <header className="App-header">

        <div className="tab-buttons">
          <button
            className={activeTab === 'invited' ? 'active' : ''}
            onClick={() => setActiveTab('invited')}
          >
            Invited
          </button>
          <button
            className={activeTab === 'accepted' ? 'active' : ''}
            onClick={() => setActiveTab('accepted')}
          >
            Accepted
          </button>
        </div>

        <div className="tab-content">
          {renderTabContent()}
        </div>
      </header>
    </div>
  );
}

export default App;