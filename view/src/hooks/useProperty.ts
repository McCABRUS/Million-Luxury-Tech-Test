import { useState, useEffect } from 'react';
import { fetchPropertyById } from '../api/properties';
import type { PropertyDetailDto } from '../api/properties';

export function useProperty(id?: string) {
  const [data, setData] = useState<PropertyDetailDto | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!id) return;
    let mounted = true;
    setLoading(true);
    fetchPropertyById(id)
      .then((r) => { if (mounted) setData(r); })
      .catch((e) => { if (mounted) setError(String(e)); })
      .finally(() => { if (mounted) setLoading(false); });
    return () => { mounted = false; };
  }, [id]);

  return { data, loading, error };
}
