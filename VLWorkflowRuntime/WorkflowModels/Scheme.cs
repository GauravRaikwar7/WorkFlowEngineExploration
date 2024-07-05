namespace VLWorkflowRuntime.WorkflowModels
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public partial class Process
    {

        private ProcessDesigner designerField;

        private ProcessActor[] actorsField;

        private ProcessParameters parametersField;

        private ProcessCommand[] commandsField;

        private ProcessTimers timersField;

        private ProcessComment[] commentsField;

        private ProcessActivity[] activitiesField;

        private ProcessTransition[] transitionsField;

        private ProcessLocalize[] localizationField;

        private string nameField;

        private bool canBeInlinedField;

        private string tagsField;

        private bool logEnabledField;

        /// <remarks/>
        public ProcessDesigner Designer
        {
            get
            {
                return designerField;
            }
            set
            {
                designerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("Actor", IsNullable = false)]
        public ProcessActor[] Actors
        {
            get
            {
                return actorsField;
            }
            set
            {
                actorsField = value;
            }
        }

        /// <remarks/>
        public ProcessParameters Parameters
        {
            get
            {
                return parametersField;
            }
            set
            {
                parametersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("Command", IsNullable = false)]
        public ProcessCommand[] Commands
        {
            get
            {
                return commandsField;
            }
            set
            {
                commandsField = value;
            }
        }

        /// <remarks/>
        public ProcessTimers Timers
        {
            get
            {
                return timersField;
            }
            set
            {
                timersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("Comment", IsNullable = false)]
        public ProcessComment[] Comments
        {
            get
            {
                return commentsField;
            }
            set
            {
                commentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("Activity", IsNullable = false)]
        public ProcessActivity[] Activities
        {
            get
            {
                return activitiesField;
            }
            set
            {
                activitiesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("Transition", IsNullable = false)]
        public ProcessTransition[] Transitions
        {
            get
            {
                return transitionsField;
            }
            set
            {
                transitionsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("Localize", IsNullable = false)]
        public ProcessLocalize[] Localization
        {
            get
            {
                return localizationField;
            }
            set
            {
                localizationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public bool CanBeInlined
        {
            get
            {
                return canBeInlinedField;
            }
            set
            {
                canBeInlinedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Tags
        {
            get
            {
                return tagsField;
            }
            set
            {
                tagsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public bool LogEnabled
        {
            get
            {
                return logEnabledField;
            }
            set
            {
                logEnabledField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessDesigner
    {

        private sbyte xField;

        private sbyte yField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public sbyte X
        {
            get
            {
                return xField;
            }
            set
            {
                xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public sbyte Y
        {
            get
            {
                return yField;
            }
            set
            {
                yField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessActor
    {

        private string nameField;

        private string ruleField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Rule
        {
            get
            {
                return ruleField;
            }
            set
            {
                ruleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessParameters
    {

        private ProcessParametersParameter parameterField;

        /// <remarks/>
        public ProcessParametersParameter Parameter
        {
            get
            {
                return parameterField;
            }
            set
            {
                parameterField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessParametersParameter
    {

        private string nameField;

        private string typeField;

        private string purposeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Type
        {
            get
            {
                return typeField;
            }
            set
            {
                typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Purpose
        {
            get
            {
                return purposeField;
            }
            set
            {
                purposeField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessCommand
    {

        private ProcessCommandInputParameters inputParametersField;

        private string nameField;

        /// <remarks/>
        public ProcessCommandInputParameters InputParameters
        {
            get
            {
                return inputParametersField;
            }
            set
            {
                inputParametersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessCommandInputParameters
    {

        private ProcessCommandInputParametersParameterRef parameterRefField;

        /// <remarks/>
        public ProcessCommandInputParametersParameterRef ParameterRef
        {
            get
            {
                return parameterRefField;
            }
            set
            {
                parameterRefField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessCommandInputParametersParameterRef
    {

        private string nameField;

        private bool isRequiredField;

        private string defaultValueField;

        private string nameRefField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public bool IsRequired
        {
            get
            {
                return isRequiredField;
            }
            set
            {
                isRequiredField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string DefaultValue
        {
            get
            {
                return defaultValueField;
            }
            set
            {
                defaultValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string NameRef
        {
            get
            {
                return nameRefField;
            }
            set
            {
                nameRefField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessTimers
    {

        private ProcessTimersTimer timerField;

        /// <remarks/>
        public ProcessTimersTimer Timer
        {
            get
            {
                return timerField;
            }
            set
            {
                timerField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessTimersTimer
    {

        private string nameField;

        private string typeField;

        private string valueField;

        private bool notOverrideIfExistsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Type
        {
            get
            {
                return typeField;
            }
            set
            {
                typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public bool NotOverrideIfExists
        {
            get
            {
                return notOverrideIfExistsField;
            }
            set
            {
                notOverrideIfExistsField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessComment
    {

        private ProcessCommentDesigner designerField;

        private string nameField;

        private string alignmentField;

        private byte rotationField;

        private float widthField;

        private bool boldTextField;

        private bool italicTextField;

        private bool underlineTextField;

        private bool lineThroughTextField;

        private byte fontSizeField;

        private string valueField;

        /// <remarks/>
        public ProcessCommentDesigner Designer
        {
            get
            {
                return designerField;
            }
            set
            {
                designerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Alignment
        {
            get
            {
                return alignmentField;
            }
            set
            {
                alignmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte Rotation
        {
            get
            {
                return rotationField;
            }
            set
            {
                rotationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public float Width
        {
            get
            {
                return widthField;
            }
            set
            {
                widthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public bool BoldText
        {
            get
            {
                return boldTextField;
            }
            set
            {
                boldTextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public bool ItalicText
        {
            get
            {
                return italicTextField;
            }
            set
            {
                italicTextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public bool UnderlineText
        {
            get
            {
                return underlineTextField;
            }
            set
            {
                underlineTextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public bool LineThroughText
        {
            get
            {
                return lineThroughTextField;
            }
            set
            {
                lineThroughTextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte FontSize
        {
            get
            {
                return fontSizeField;
            }
            set
            {
                fontSizeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessCommentDesigner
    {

        private float xField;

        private float yField;

        private string colorField;

        private bool hiddenField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public float X
        {
            get
            {
                return xField;
            }
            set
            {
                xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public float Y
        {
            get
            {
                return yField;
            }
            set
            {
                yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Color
        {
            get
            {
                return colorField;
            }
            set
            {
                colorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public bool Hidden
        {
            get
            {
                return hiddenField;
            }
            set
            {
                hiddenField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessActivity
    {

        private ProcessActivityDesigner designerField;

        private string nameField;

        private string stateField;

        private bool isInitialField;

        private bool isFinalField;

        private bool isForSetStateField;

        private bool isAutoSchemeUpdateField;

        /// <remarks/>
        public ProcessActivityDesigner Designer
        {
            get
            {
                return designerField;
            }
            set
            {
                designerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string State
        {
            get
            {
                return stateField;
            }
            set
            {
                stateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public bool IsInitial
        {
            get
            {
                return isInitialField;
            }
            set
            {
                isInitialField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public bool IsFinal
        {
            get
            {
                return isFinalField;
            }
            set
            {
                isFinalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public bool IsForSetState
        {
            get
            {
                return isForSetStateField;
            }
            set
            {
                isForSetStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public bool IsAutoSchemeUpdate
        {
            get
            {
                return isAutoSchemeUpdateField;
            }
            set
            {
                isAutoSchemeUpdateField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessActivityDesigner
    {

        private float xField;

        private float yField;

        private string colorField;

        private bool hiddenField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public float X
        {
            get
            {
                return xField;
            }
            set
            {
                xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public float Y
        {
            get
            {
                return yField;
            }
            set
            {
                yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Color
        {
            get
            {
                return colorField;
            }
            set
            {
                colorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public bool Hidden
        {
            get
            {
                return hiddenField;
            }
            set
            {
                hiddenField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessTransition
    {

        private ProcessTransitionRestrictions restrictionsField;

        private ProcessTransitionTriggers triggersField;

        private ProcessTransitionConditions conditionsField;

        private ProcessTransitionDesigner designerField;

        private string nameField;

        private string toField;

        private string fromField;

        private string classifierField;

        private string allowConcatenationTypeField;

        private string restrictConcatenationTypeField;

        private string conditionsConcatenationTypeField;

        private bool disableParentStateControlField;

        /// <remarks/>
        public ProcessTransitionRestrictions Restrictions
        {
            get
            {
                return restrictionsField;
            }
            set
            {
                restrictionsField = value;
            }
        }

        /// <remarks/>
        public ProcessTransitionTriggers Triggers
        {
            get
            {
                return triggersField;
            }
            set
            {
                triggersField = value;
            }
        }

        /// <remarks/>
        public ProcessTransitionConditions Conditions
        {
            get
            {
                return conditionsField;
            }
            set
            {
                conditionsField = value;
            }
        }

        /// <remarks/>
        public ProcessTransitionDesigner Designer
        {
            get
            {
                return designerField;
            }
            set
            {
                designerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string To
        {
            get
            {
                return toField;
            }
            set
            {
                toField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string From
        {
            get
            {
                return fromField;
            }
            set
            {
                fromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Classifier
        {
            get
            {
                return classifierField;
            }
            set
            {
                classifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string AllowConcatenationType
        {
            get
            {
                return allowConcatenationTypeField;
            }
            set
            {
                allowConcatenationTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string RestrictConcatenationType
        {
            get
            {
                return restrictConcatenationTypeField;
            }
            set
            {
                restrictConcatenationTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string ConditionsConcatenationType
        {
            get
            {
                return conditionsConcatenationTypeField;
            }
            set
            {
                conditionsConcatenationTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public bool DisableParentStateControl
        {
            get
            {
                return disableParentStateControlField;
            }
            set
            {
                disableParentStateControlField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessTransitionRestrictions
    {

        private ProcessTransitionRestrictionsRestriction restrictionField;

        /// <remarks/>
        public ProcessTransitionRestrictionsRestriction Restriction
        {
            get
            {
                return restrictionField;
            }
            set
            {
                restrictionField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessTransitionRestrictionsRestriction
    {

        private string typeField;

        private string nameRefField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Type
        {
            get
            {
                return typeField;
            }
            set
            {
                typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string NameRef
        {
            get
            {
                return nameRefField;
            }
            set
            {
                nameRefField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessTransitionTriggers
    {

        private ProcessTransitionTriggersTrigger triggerField;

        /// <remarks/>
        public ProcessTransitionTriggersTrigger Trigger
        {
            get
            {
                return triggerField;
            }
            set
            {
                triggerField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessTransitionTriggersTrigger
    {

        private string typeField;

        private string nameRefField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Type
        {
            get
            {
                return typeField;
            }
            set
            {
                typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string NameRef
        {
            get
            {
                return nameRefField;
            }
            set
            {
                nameRefField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessTransitionConditions
    {

        private ProcessTransitionConditionsCondition conditionField;

        /// <remarks/>
        public ProcessTransitionConditionsCondition Condition
        {
            get
            {
                return conditionField;
            }
            set
            {
                conditionField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessTransitionConditionsCondition
    {

        private string expressionField;

        private string typeField;

        private bool conditionInversionField;

        private bool conditionInversionFieldSpecified;

        /// <remarks/>
        public string Expression
        {
            get
            {
                return expressionField;
            }
            set
            {
                expressionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Type
        {
            get
            {
                return typeField;
            }
            set
            {
                typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public bool ConditionInversion
        {
            get
            {
                return conditionInversionField;
            }
            set
            {
                conditionInversionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ConditionInversionSpecified
        {
            get
            {
                return conditionInversionFieldSpecified;
            }
            set
            {
                conditionInversionFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessTransitionDesigner
    {

        private float xField;

        private bool xFieldSpecified;

        private float yField;

        private bool yFieldSpecified;

        private bool hiddenField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public float X
        {
            get
            {
                return xField;
            }
            set
            {
                xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool XSpecified
        {
            get
            {
                return xFieldSpecified;
            }
            set
            {
                xFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public float Y
        {
            get
            {
                return yField;
            }
            set
            {
                yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool YSpecified
        {
            get
            {
                return yFieldSpecified;
            }
            set
            {
                yFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public bool Hidden
        {
            get
            {
                return hiddenField;
            }
            set
            {
                hiddenField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class ProcessLocalize
    {

        private string typeField;

        private string isDefaultField;

        private string cultureField;

        private string objectNameField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Type
        {
            get
            {
                return typeField;
            }
            set
            {
                typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IsDefault
        {
            get
            {
                return isDefaultField;
            }
            set
            {
                isDefaultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Culture
        {
            get
            {
                return cultureField;
            }
            set
            {
                cultureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string ObjectName
        {
            get
            {
                return objectNameField;
            }
            set
            {
                objectNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }
    }
}
