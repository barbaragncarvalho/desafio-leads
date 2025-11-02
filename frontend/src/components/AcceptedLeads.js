import React from 'react';
import './LeadCard.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCircleUser } from '@fortawesome/free-solid-svg-icons';

function AcceptedLeads({ leads }) {

    if (!leads || leads.length === 0) {
        return <p>Nenhum lead aceito encontrado.</p>;
    }

    return (
        <div className="lead-list">
            {leads.map(lead => (
                <div key={lead.id} className="lead-card">

                    <div className="card-header">
                        <span className="contact-name">
                            <FontAwesomeIcon icon={faCircleUser} style={{ color: "#ec9909", marginRight: "10px" }} />
                            {lead.contactFullName}
                        </span>
                        <span className="date-created">{new Date(lead.dateCreated).toLocaleString()}</span>
                    </div>

                    <div className="card-body">
                        <p className="suburb-category">
                            📍 {lead.suburb} &nbsp; | &nbsp; 💼 {lead.category} &nbsp; | &nbsp; 🆔 Job ID: {lead.id}
                        </p>
                        <p className="contact-info">
                            📞 {lead.contactPhoneNumber} &nbsp; | &nbsp; ✉️ {lead.contactEmail}
                        </p>
                        <p className="description">{lead.description}</p>
                    </div>

                    <div className="card-footer">
                        <span className="price">${lead.price.toFixed(2)} Lead Invitation</span>
                    </div>

                </div>
            ))}
        </div>
    );
}

export default AcceptedLeads;