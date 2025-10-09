import { useState, useEffect } from 'react';
import { fetchProperties, PropertyListParams, PropertyListDto } from '../api/properties';

export function useProperties(initialParams: PropertyListParams = {}) {
  const [params, setParams] = useState<PropertyListParams>(initialParams);
  const [data, setData] = useState<PropertyListDto[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    let mounted = true;
    setLoading(true);
    fetchProperties(params)
      .then(r => { if (mounted) setData(r); })
      .catch(e => { if (mounted) setError(String(e)); })
      .finally(() => { if (mounted) setLoading(false); });
    return () => { mounted = false; };
  }, [JSON.stringify(params)]);

  return { data, loading, error, params, setParams };
}