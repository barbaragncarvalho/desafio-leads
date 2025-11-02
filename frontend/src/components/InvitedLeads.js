import React from 'react';
import './LeadCard.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCircleUser } from '@fortawesome/free-solid-svg-icons';

function InvitedLeads({ leads, updatingId, onAccept, onDecline }) {

    if (!leads || leads.length === 0) {
        return <p>Nenhum lead convidado encontrado.</p>;
    }

    return (
        <div className="lead-list">
            {leads.map(lead => (
                <div key={lead.id} className="lead-card">

                    <div className="card-header">
                        <span className="contact-name">
                            <FontAwesomeIcon icon={faCircleUser} style={{ color: "#ec9909", marginRight: "10px" }} />
                            {lead.contactFirstName}
                        </span>
                        <span className="date-created">{new Date(lead.dateCreated).toLocaleString()}</span>
                    </div>

                    <div className="card-body">
                        <p className="suburb-category">
                            📍 {lead.suburb} &nbsp; | &nbsp; 💼 {lead.category} &nbsp; | &nbsp; 🆔 Job ID: {lead.id}
                        </p>
                        <p className="description">{lead.description}</p>
                    </div>

                    <div className="card-footer">
                        <div>
                            {}
                            <button
                                onClick={() => onAccept(lead.id)}
                                className="btn-accept"
                                disabled={updatingId === lead.id}
                            >
                                {}
                                {updatingId === lead.id ? '...' : 'Accept'}
                            </button>
                            <button
                                onClick={() => onDecline(lead.id)}
                                className="btn-decline"
                                disabled={updatingId === lead.id}
                            >
                                {updatingId === lead.id ? '...' : 'Decline'}
                            </button>
                        </div>
                        <span className="price">${lead.price.toFixed(2)} Lead Invitation</span>
                    </div>

                </div>
            ))}
        </div>
    );
}

export default InvitedLeads;