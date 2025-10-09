import React from 'react';
import { Link } from 'react-router-dom';
type Props = { prop: any };
export default function PropertyCard({ prop }: Props) {
  return (
    <article className="card">
      <Link to={`/properties/${prop.idProperty}`}>
        <img src={prop.image ?? '/placeholder.png'} alt={prop.name} />
        <h3>{prop.name}</h3>
        <p>{prop.address}</p>
        <p><strong>{prop.price.toLocaleString()}</strong></p>
      </Link>
    </article>
  );
}