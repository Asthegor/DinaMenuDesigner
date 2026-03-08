using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DinaMenuDesigner.Common
{
    /// <summary>
    /// Classe de base pour tous les objets qui doivent notifier l'interface WPF
    /// lorsqu'une de leurs propriétés change de valeur.
    /// Équivalent WPF de l'événement Worksheet_Change en VBA.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Événement déclenché automatiquement par WPF chaque fois qu'une propriété change.
        /// WPF s'abonne à cet événement pour savoir quand mettre à jour l'affichage.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notifie WPF qu'une propriété a changé de valeur.
        /// Grâce à [CallerMemberName], il n'est pas nécessaire de passer le nom
        /// de la propriété manuellement — le compilateur le renseigne automatiquement.
        /// Exemple d'appel depuis un setter : OnPropertyChanged();
        /// </summary>
        /// <param name="propertyName">
        /// Nom de la propriété qui a changé.
        /// Rempli automatiquement par le compilateur via [CallerMemberName].
        /// </param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            // On s'assure que la notification est toujours exécutée sur le thread UI.
            // Si on est déjà sur le thread UI (cas habituel), Invoke exécute directement.
            // Si on est sur un autre thread (ex : chargement JSON async), Invoke
            // transmet la notification au thread UI avant de l'exécuter.
            if (Application.Current?.Dispatcher.CheckAccess() == true)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            else
                Application.Current?.Dispatcher.Invoke(() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)));
        }

        /// <summary>
        /// Affecte une nouvelle valeur à un champ privé et notifie WPF si la valeur
        /// a réellement changé. Évite les notifications inutiles si la valeur est identique.
        /// Exemple d'appel depuis un setter : SetProperty(ref _content, value);
        /// </summary>
        /// <typeparam name="T">Le type de la propriété.</typeparam>
        /// <param name="field">Référence au champ privé (la variable _maVariable).</param>
        /// <param name="value">La nouvelle valeur à affecter.</param>
        /// <param name="propertyName">
        /// Nom de la propriété. Rempli automatiquement par le compilateur.
        /// </param>
        /// <returns>
        /// true si la valeur a changé et que WPF a été notifié.
        /// false si la valeur était déjà identique — aucune notification envoyée.
        /// </returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            // Si l'ancienne et la nouvelle valeur sont identiques, on ne fait rien.
            // Cela évite de déclencher des mises à jour d'affichage inutiles.
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}