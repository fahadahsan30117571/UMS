import React from 'react';
import { IonContent, IonHeader, IonPage, IonTitle, IonToolbar } from '@ionic/react';
import UserForm from '../components/UserForm'; // Assuming UserForm component is in the components directory
import './Tab1.css';

const Tab1: React.FC = () => {
  const handleSubmit = async (userData: any) => {
    try {
      const response = await fetch('https://localhost:7208/api/Student', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(userData),
      });

      if (!response.ok) {
        throw new Error('Failed to submit user data');
      }

      console.log('User data submitted successfully');
    } catch (error) {
      console.error('Error submitting user data:', error.message);
    }
  };

  return (
    <IonPage>
      <IonHeader>
        <IonToolbar>
          <IonTitle>USW Student Portal</IonTitle>
        </IonToolbar>
      </IonHeader>
      <IonContent fullscreen>
        <UserForm onSubmit={handleSubmit} />
      </IonContent>
    </IonPage>
  );
};

export default Tab1;
