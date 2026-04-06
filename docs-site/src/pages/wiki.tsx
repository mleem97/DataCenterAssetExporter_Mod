import React, {useEffect} from 'react';
import Layout from '@theme/Layout';
import Link from '@docusaurus/Link';

export default function WikiLandingPage(): JSX.Element {
  useEffect(() => {
    if (typeof window !== 'undefined') {
      window.location.replace('/wiki/docs');
    }
  }, []);

  return (
    <Layout title="Wiki" description="Wiki entrypoint">
      <main className="bg-app-bg min-h-screen text-gray-200 px-4 py-16">
        <section className="mx-auto max-w-3xl text-center app-card app-card-glow rounded-xl p-8">
          <h1 className="text-3xl font-bold text-white mb-3">Wiki</h1>
          <p className="text-gray-400 mb-6">Redirecting to the wiki overview...</p>
          <Link to="/wiki/docs" className="button button--primary">
            Open Wiki Overview
          </Link>
        </section>
      </main>
    </Layout>
  );
}
