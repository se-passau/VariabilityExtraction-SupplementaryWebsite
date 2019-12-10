﻿using System.Windows.Forms;

namespace PerformancePrediction_GUI
{
    partial class PerformancePrediction_Frame
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (executionThread != null && executionThread.IsAlive)
            {
                executionThread.Abort();
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.BinarySamplingGroup = new System.Windows.Forms.GroupBox();
            this.binRandomSeed = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numConfigsParam = new System.Windows.Forms.TextBox();
            this.numConfigs = new System.Windows.Forms.Label();
            this.twiseParam = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.binRandom = new System.Windows.Forms.CheckBox();
            this.twise = new System.Windows.Forms.CheckBox();
            this.binWholePop = new System.Windows.Forms.CheckBox();
            this.negOW = new System.Windows.Forms.CheckBox();
            this.PW = new System.Windows.Forms.CheckBox();
            this.OW = new System.Windows.Forms.CheckBox();
            this.LogBox = new System.Windows.Forms.TextBox();
            this.LogGroup = new System.Windows.Forms.GroupBox();
            this.StartLearningButton = new System.Windows.Forms.Button();
            this.expDasign_group = new System.Windows.Forms.GroupBox();
            this.num_oneFactorAtATime_num_Text = new System.Windows.Forms.TextBox();
            this.num_oneFactorAtATime_num_Label = new System.Windows.Forms.Label();
            this.num_oneFactorAtATime_Box = new System.Windows.Forms.CheckBox();
            this.num_rand_seed_Text = new System.Windows.Forms.TextBox();
            this.num_rand_seed_Label = new System.Windows.Forms.Label();
            this.num_Plackett_n_Box = new System.Windows.Forms.TextBox();
            this.num_Plackett_n_Label = new System.Windows.Forms.Label();
            this.num_Plackett_Level_Box = new System.Windows.Forms.TextBox();
            this.num_Plackett_Level_Label = new System.Windows.Forms.Label();
            this.num_hyper_percent_text = new System.Windows.Forms.TextBox();
            this.num_hyper_percent_Label = new System.Windows.Forms.Label();
            this.num_random_n_Text = new System.Windows.Forms.TextBox();
            this.num_rand_n_Label = new System.Windows.Forms.Label();
            this.num_kEx_k_Box = new System.Windows.Forms.TextBox();
            this.num_kEx_k_Label = new System.Windows.Forms.Label();
            this.num_kEx_n_Box = new System.Windows.Forms.TextBox();
            this.num_kEx_n_Label = new System.Windows.Forms.Label();
            this.num_hyperSampling_check = new System.Windows.Forms.CheckBox();
            this.num_randomSampling_num = new System.Windows.Forms.CheckBox();
            this.num_PlackettBurman_check = new System.Windows.Forms.CheckBox();
            this.num_kEx_check = new System.Windows.Forms.CheckBox();
            this.num_FullFactorial_check = new System.Windows.Forms.CheckBox();
            this.num_CentralComposite_check = new System.Windows.Forms.CheckBox();
            this.num_BoxBehnken_check = new System.Windows.Forms.CheckBox();
            this.perfInfGridView = new System.Windows.Forms.DataGridView();
            this.pim_group = new System.Windows.Forms.GroupBox();
            this.nfpSelection = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MlSettings_Box = new System.Windows.Forms.GroupBox();
            this.mlSettingsPanel = new System.Windows.Forms.Panel();
            this.readMeasurements = new System.Windows.Forms.Button();
            this.cleanButton = new System.Windows.Forms.Button();
            this.readVarModel = new System.Windows.Forms.Button();
            this.LearnAllMeasurements = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.PrintConfigsGroupBox = new System.Windows.Forms.GroupBox();
            this.PrintconfigsButton = new System.Windows.Forms.Button();
            this.PostfixLabel = new System.Windows.Forms.Label();
            this.PrefixLabel = new System.Windows.Forms.Label();
            this.PostFixTextBox = new System.Windows.Forms.TextBox();
            this.PrefixTextBox = new System.Windows.Forms.TextBox();
            this.BinarySamplingGroup.SuspendLayout();
            this.LogGroup.SuspendLayout();
            this.expDasign_group.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.perfInfGridView)).BeginInit();
            this.pim_group.SuspendLayout();
            this.MlSettings_Box.SuspendLayout();
            this.PrintConfigsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // BinarySamplingGroup
            // 
            this.BinarySamplingGroup.Controls.Add(this.binRandomSeed);
            this.BinarySamplingGroup.Controls.Add(this.label3);
            this.BinarySamplingGroup.Controls.Add(this.numConfigsParam);
            this.BinarySamplingGroup.Controls.Add(this.numConfigs);
            this.BinarySamplingGroup.Controls.Add(this.twiseParam);
            this.BinarySamplingGroup.Controls.Add(this.label2);
            this.BinarySamplingGroup.Controls.Add(this.binRandom);
            this.BinarySamplingGroup.Controls.Add(this.twise);
            this.BinarySamplingGroup.Controls.Add(this.binWholePop);
            this.BinarySamplingGroup.Controls.Add(this.negOW);
            this.BinarySamplingGroup.Controls.Add(this.PW);
            this.BinarySamplingGroup.Controls.Add(this.OW);
            this.BinarySamplingGroup.Location = new System.Drawing.Point(132, 7);
            this.BinarySamplingGroup.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BinarySamplingGroup.Name = "BinarySamplingGroup";
            this.BinarySamplingGroup.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BinarySamplingGroup.Size = new System.Drawing.Size(538, 82);
            this.BinarySamplingGroup.TabIndex = 0;
            this.BinarySamplingGroup.TabStop = false;
            this.BinarySamplingGroup.Text = "Binary Options: Sampling Heuristics ";
            // 
            // binRandomSeed
            // 
            this.binRandomSeed.Location = new System.Drawing.Point(503, 55);
            this.binRandomSeed.Name = "binRandomSeed";
            this.binRandomSeed.Size = new System.Drawing.Size(30, 25);
            this.binRandomSeed.TabIndex = 11;
            this.binRandomSeed.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(463, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "seed:";
            // 
            // numConfigsParam
            // 
            this.numConfigsParam.Location = new System.Drawing.Point(411, 55);
            this.numConfigsParam.Name = "numConfigsParam";
            this.numConfigsParam.Size = new System.Drawing.Size(46, 25);
            this.numConfigsParam.TabIndex = 9;
            this.numConfigsParam.Text = "asOW";
            // 
            // numConfigs
            // 
            this.numConfigs.AutoSize = true;
            this.numConfigs.Location = new System.Drawing.Point(327, 57);
            this.numConfigs.Name = "numConfigs";
            this.numConfigs.Size = new System.Drawing.Size(81, 16);
            this.numConfigs.TabIndex = 8;
            this.numConfigs.Text = "numConfigs:";
            // 
            // twiseParam
            // 
            this.twiseParam.Location = new System.Drawing.Point(108, 55);
            this.twiseParam.Name = "twiseParam";
            this.twiseParam.Size = new System.Drawing.Size(22, 25);
            this.twiseParam.TabIndex = 7;
            this.twiseParam.Text = "3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "t:";
            // 
            // binRandom
            // 
            this.binRandom.AutoSize = true;
            this.binRandom.Location = new System.Drawing.Point(204, 55);
            this.binRandom.Name = "binRandom";
            this.binRandom.Size = new System.Drawing.Size(122, 20);
            this.binRandom.TabIndex = 5;
            this.binRandom.Text = "Binary Random";
            this.binRandom.UseVisualStyleBackColor = true;
            // 
            // twise
            // 
            this.twise.AutoSize = true;
            this.twise.Location = new System.Drawing.Point(8, 55);
            this.twise.Name = "twise";
            this.twise.Size = new System.Drawing.Size(72, 20);
            this.twise.TabIndex = 4;
            this.twise.Text = "T-Wise";
            this.twise.UseVisualStyleBackColor = true;
            // 
            // binWholePop
            // 
            this.binWholePop.AutoSize = true;
            this.binWholePop.Location = new System.Drawing.Point(359, 21);
            this.binWholePop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.binWholePop.Name = "binWholePop";
            this.binWholePop.Size = new System.Drawing.Size(133, 20);
            this.binWholePop.TabIndex = 3;
            this.binWholePop.Text = "Whole Population";
            this.binWholePop.UseVisualStyleBackColor = true;
            // 
            // negOW
            // 
            this.negOW.AutoSize = true;
            this.negOW.Location = new System.Drawing.Point(204, 21);
            this.negOW.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.negOW.Name = "negOW";
            this.negOW.Size = new System.Drawing.Size(156, 20);
            this.negOW.TabIndex = 2;
            this.negOW.Text = "negative Option-Wise";
            this.negOW.UseVisualStyleBackColor = true;
            // 
            // PW
            // 
            this.PW.AutoSize = true;
            this.PW.Location = new System.Drawing.Point(114, 21);
            this.PW.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.PW.Name = "PW";
            this.PW.Size = new System.Drawing.Size(88, 20);
            this.PW.TabIndex = 1;
            this.PW.Text = "Pair-Wise";
            this.PW.UseVisualStyleBackColor = true;
            // 
            // OW
            // 
            this.OW.AutoSize = true;
            this.OW.Location = new System.Drawing.Point(8, 21);
            this.OW.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.OW.Name = "OW";
            this.OW.Size = new System.Drawing.Size(103, 20);
            this.OW.TabIndex = 0;
            this.OW.Text = "Option-Wise";
            this.OW.UseVisualStyleBackColor = true;
            // 
            // LogBox
            // 
            this.LogBox.Location = new System.Drawing.Point(8, 21);
            this.LogBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LogBox.Multiline = true;
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogBox.Size = new System.Drawing.Size(1198, 75);
            this.LogBox.TabIndex = 2;
            // 
            // LogGroup
            // 
            this.LogGroup.Controls.Add(this.LogBox);
            this.LogGroup.Location = new System.Drawing.Point(15, 763);
            this.LogGroup.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LogGroup.Name = "LogGroup";
            this.LogGroup.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LogGroup.Size = new System.Drawing.Size(1212, 103);
            this.LogGroup.TabIndex = 3;
            this.LogGroup.TabStop = false;
            this.LogGroup.Text = "Log";
            // 
            // StartLearningButton
            // 
            this.StartLearningButton.Location = new System.Drawing.Point(700, 291);
            this.StartLearningButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.StartLearningButton.Name = "StartLearningButton";
            this.StartLearningButton.Size = new System.Drawing.Size(151, 54);
            this.StartLearningButton.TabIndex = 4;
            this.StartLearningButton.Text = "Start Learning";
            this.StartLearningButton.UseVisualStyleBackColor = true;
            this.StartLearningButton.Click += new System.EventHandler(this.StartLearningButton_Click);
            // 
            // expDasign_group
            // 
            this.expDasign_group.Controls.Add(this.num_oneFactorAtATime_num_Text);
            this.expDasign_group.Controls.Add(this.num_oneFactorAtATime_num_Label);
            this.expDasign_group.Controls.Add(this.num_oneFactorAtATime_Box);
            this.expDasign_group.Controls.Add(this.num_rand_seed_Text);
            this.expDasign_group.Controls.Add(this.num_rand_seed_Label);
            this.expDasign_group.Controls.Add(this.num_Plackett_n_Box);
            this.expDasign_group.Controls.Add(this.num_Plackett_n_Label);
            this.expDasign_group.Controls.Add(this.num_Plackett_Level_Box);
            this.expDasign_group.Controls.Add(this.num_Plackett_Level_Label);
            this.expDasign_group.Controls.Add(this.num_hyper_percent_text);
            this.expDasign_group.Controls.Add(this.num_hyper_percent_Label);
            this.expDasign_group.Controls.Add(this.num_random_n_Text);
            this.expDasign_group.Controls.Add(this.num_rand_n_Label);
            this.expDasign_group.Controls.Add(this.num_kEx_k_Box);
            this.expDasign_group.Controls.Add(this.num_kEx_k_Label);
            this.expDasign_group.Controls.Add(this.num_kEx_n_Box);
            this.expDasign_group.Controls.Add(this.num_kEx_n_Label);
            this.expDasign_group.Controls.Add(this.num_hyperSampling_check);
            this.expDasign_group.Controls.Add(this.num_randomSampling_num);
            this.expDasign_group.Controls.Add(this.num_PlackettBurman_check);
            this.expDasign_group.Controls.Add(this.num_kEx_check);
            this.expDasign_group.Controls.Add(this.num_FullFactorial_check);
            this.expDasign_group.Controls.Add(this.num_CentralComposite_check);
            this.expDasign_group.Controls.Add(this.num_BoxBehnken_check);
            this.expDasign_group.Location = new System.Drawing.Point(132, 95);
            this.expDasign_group.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.expDasign_group.Name = "expDasign_group";
            this.expDasign_group.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.expDasign_group.Size = new System.Drawing.Size(538, 173);
            this.expDasign_group.TabIndex = 8;
            this.expDasign_group.TabStop = false;
            this.expDasign_group.Text = "Numeric Options: Experimental Designs";
            // 
            // num_oneFactorAtATime_num_Text
            // 
            this.num_oneFactorAtATime_num_Text.Location = new System.Drawing.Point(239, 145);
            this.num_oneFactorAtATime_num_Text.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_oneFactorAtATime_num_Text.Name = "num_oneFactorAtATime_num_Text";
            this.num_oneFactorAtATime_num_Text.Size = new System.Drawing.Size(41, 25);
            this.num_oneFactorAtATime_num_Text.TabIndex = 34;
            this.num_oneFactorAtATime_num_Text.Text = "5";
            this.num_oneFactorAtATime_num_Text.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // num_oneFactorAtATime_num_Label
            // 
            this.num_oneFactorAtATime_num_Label.AutoSize = true;
            this.num_oneFactorAtATime_num_Label.Location = new System.Drawing.Point(182, 147);
            this.num_oneFactorAtATime_num_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.num_oneFactorAtATime_num_Label.Name = "num_oneFactorAtATime_num_Label";
            this.num_oneFactorAtATime_num_Label.Size = new System.Drawing.Size(48, 16);
            this.num_oneFactorAtATime_num_Label.TabIndex = 33;
            this.num_oneFactorAtATime_num_Label.Text = "values:";
            // 
            // num_oneFactorAtATime_Box
            // 
            this.num_oneFactorAtATime_Box.AutoSize = true;
            this.num_oneFactorAtATime_Box.Location = new System.Drawing.Point(8, 147);
            this.num_oneFactorAtATime_Box.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_oneFactorAtATime_Box.Name = "num_oneFactorAtATime_Box";
            this.num_oneFactorAtATime_Box.Size = new System.Drawing.Size(156, 20);
            this.num_oneFactorAtATime_Box.TabIndex = 32;
            this.num_oneFactorAtATime_Box.Text = "One Factor at a Time";
            this.num_oneFactorAtATime_Box.UseVisualStyleBackColor = true;
            // 
            // num_rand_seed_Text
            // 
            this.num_rand_seed_Text.Location = new System.Drawing.Point(330, 99);
            this.num_rand_seed_Text.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_rand_seed_Text.Name = "num_rand_seed_Text";
            this.num_rand_seed_Text.Size = new System.Drawing.Size(38, 25);
            this.num_rand_seed_Text.TabIndex = 31;
            this.num_rand_seed_Text.Text = "0";
            this.num_rand_seed_Text.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // num_rand_seed_Label
            // 
            this.num_rand_seed_Label.AutoSize = true;
            this.num_rand_seed_Label.Location = new System.Drawing.Point(293, 99);
            this.num_rand_seed_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.num_rand_seed_Label.Name = "num_rand_seed_Label";
            this.num_rand_seed_Label.Size = new System.Drawing.Size(38, 16);
            this.num_rand_seed_Label.TabIndex = 30;
            this.num_rand_seed_Label.Text = "seed:";
            // 
            // num_Plackett_n_Box
            // 
            this.num_Plackett_n_Box.Location = new System.Drawing.Point(330, 71);
            this.num_Plackett_n_Box.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_Plackett_n_Box.Name = "num_Plackett_n_Box";
            this.num_Plackett_n_Box.Size = new System.Drawing.Size(38, 25);
            this.num_Plackett_n_Box.TabIndex = 29;
            this.num_Plackett_n_Box.Text = "9";
            this.num_Plackett_n_Box.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // num_Plackett_n_Label
            // 
            this.num_Plackett_n_Label.AutoSize = true;
            this.num_Plackett_n_Label.Location = new System.Drawing.Point(293, 73);
            this.num_Plackett_n_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.num_Plackett_n_Label.Name = "num_Plackett_n_Label";
            this.num_Plackett_n_Label.Size = new System.Drawing.Size(18, 16);
            this.num_Plackett_n_Label.TabIndex = 28;
            this.num_Plackett_n_Label.Text = "n:";
            // 
            // num_Plackett_Level_Box
            // 
            this.num_Plackett_Level_Box.Location = new System.Drawing.Point(239, 71);
            this.num_Plackett_Level_Box.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_Plackett_Level_Box.Name = "num_Plackett_Level_Box";
            this.num_Plackett_Level_Box.Size = new System.Drawing.Size(41, 25);
            this.num_Plackett_Level_Box.TabIndex = 27;
            this.num_Plackett_Level_Box.Text = "3";
            this.num_Plackett_Level_Box.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // num_Plackett_Level_Label
            // 
            this.num_Plackett_Level_Label.AutoSize = true;
            this.num_Plackett_Level_Label.Location = new System.Drawing.Point(182, 73);
            this.num_Plackett_Level_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.num_Plackett_Level_Label.Name = "num_Plackett_Level_Label";
            this.num_Plackett_Level_Label.Size = new System.Drawing.Size(38, 16);
            this.num_Plackett_Level_Label.TabIndex = 26;
            this.num_Plackett_Level_Label.Text = "level:";
            // 
            // num_hyper_percent_text
            // 
            this.num_hyper_percent_text.Location = new System.Drawing.Point(239, 119);
            this.num_hyper_percent_text.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_hyper_percent_text.Name = "num_hyper_percent_text";
            this.num_hyper_percent_text.Size = new System.Drawing.Size(41, 25);
            this.num_hyper_percent_text.TabIndex = 25;
            this.num_hyper_percent_text.Text = "10";
            this.num_hyper_percent_text.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // num_hyper_percent_Label
            // 
            this.num_hyper_percent_Label.AutoSize = true;
            this.num_hyper_percent_Label.Location = new System.Drawing.Point(182, 121);
            this.num_hyper_percent_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.num_hyper_percent_Label.Name = "num_hyper_percent_Label";
            this.num_hyper_percent_Label.Size = new System.Drawing.Size(55, 16);
            this.num_hyper_percent_Label.TabIndex = 24;
            this.num_hyper_percent_Label.Text = "percent:";
            // 
            // num_random_n_Text
            // 
            this.num_random_n_Text.Location = new System.Drawing.Point(239, 96);
            this.num_random_n_Text.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_random_n_Text.Name = "num_random_n_Text";
            this.num_random_n_Text.Size = new System.Drawing.Size(41, 25);
            this.num_random_n_Text.TabIndex = 23;
            this.num_random_n_Text.Text = "100";
            this.num_random_n_Text.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // num_rand_n_Label
            // 
            this.num_rand_n_Label.AutoSize = true;
            this.num_rand_n_Label.Location = new System.Drawing.Point(182, 99);
            this.num_rand_n_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.num_rand_n_Label.Name = "num_rand_n_Label";
            this.num_rand_n_Label.Size = new System.Drawing.Size(18, 16);
            this.num_rand_n_Label.TabIndex = 22;
            this.num_rand_n_Label.Text = "n:";
            // 
            // num_kEx_k_Box
            // 
            this.num_kEx_k_Box.Location = new System.Drawing.Point(330, 44);
            this.num_kEx_k_Box.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_kEx_k_Box.Name = "num_kEx_k_Box";
            this.num_kEx_k_Box.Size = new System.Drawing.Size(38, 25);
            this.num_kEx_k_Box.TabIndex = 21;
            this.num_kEx_k_Box.Text = "5";
            this.num_kEx_k_Box.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // num_kEx_k_Label
            // 
            this.num_kEx_k_Label.AutoSize = true;
            this.num_kEx_k_Label.Location = new System.Drawing.Point(293, 48);
            this.num_kEx_k_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.num_kEx_k_Label.Name = "num_kEx_k_Label";
            this.num_kEx_k_Label.Size = new System.Drawing.Size(18, 16);
            this.num_kEx_k_Label.TabIndex = 20;
            this.num_kEx_k_Label.Text = "k:";
            // 
            // num_kEx_n_Box
            // 
            this.num_kEx_n_Box.Location = new System.Drawing.Point(239, 44);
            this.num_kEx_n_Box.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_kEx_n_Box.Name = "num_kEx_n_Box";
            this.num_kEx_n_Box.Size = new System.Drawing.Size(41, 25);
            this.num_kEx_n_Box.TabIndex = 19;
            this.num_kEx_n_Box.Text = "50";
            this.num_kEx_n_Box.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // num_kEx_n_Label
            // 
            this.num_kEx_n_Label.AutoSize = true;
            this.num_kEx_n_Label.Location = new System.Drawing.Point(182, 47);
            this.num_kEx_n_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.num_kEx_n_Label.Name = "num_kEx_n_Label";
            this.num_kEx_n_Label.Size = new System.Drawing.Size(18, 16);
            this.num_kEx_n_Label.TabIndex = 18;
            this.num_kEx_n_Label.Text = "n:";
            // 
            // num_hyperSampling_check
            // 
            this.num_hyperSampling_check.AutoSize = true;
            this.num_hyperSampling_check.Location = new System.Drawing.Point(8, 121);
            this.num_hyperSampling_check.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_hyperSampling_check.Name = "num_hyperSampling_check";
            this.num_hyperSampling_check.Size = new System.Drawing.Size(122, 20);
            this.num_hyperSampling_check.TabIndex = 15;
            this.num_hyperSampling_check.Text = "Hyper sampling";
            this.num_hyperSampling_check.UseVisualStyleBackColor = true;
            // 
            // num_randomSampling_num
            // 
            this.num_randomSampling_num.AutoSize = true;
            this.num_randomSampling_num.Location = new System.Drawing.Point(8, 97);
            this.num_randomSampling_num.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_randomSampling_num.Name = "num_randomSampling_num";
            this.num_randomSampling_num.Size = new System.Drawing.Size(134, 20);
            this.num_randomSampling_num.TabIndex = 14;
            this.num_randomSampling_num.Text = "Random sampling";
            this.num_randomSampling_num.UseVisualStyleBackColor = true;
            // 
            // num_PlackettBurman_check
            // 
            this.num_PlackettBurman_check.AutoSize = true;
            this.num_PlackettBurman_check.Location = new System.Drawing.Point(8, 71);
            this.num_PlackettBurman_check.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_PlackettBurman_check.Name = "num_PlackettBurman_check";
            this.num_PlackettBurman_check.Size = new System.Drawing.Size(130, 20);
            this.num_PlackettBurman_check.TabIndex = 13;
            this.num_PlackettBurman_check.Text = "Plackett-Burman";
            this.num_PlackettBurman_check.UseVisualStyleBackColor = true;
            // 
            // num_kEx_check
            // 
            this.num_kEx_check.AutoSize = true;
            this.num_kEx_check.Location = new System.Drawing.Point(8, 47);
            this.num_kEx_check.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_kEx_check.Name = "num_kEx_check";
            this.num_kEx_check.Size = new System.Drawing.Size(176, 20);
            this.num_kEx_check.TabIndex = 12;
            this.num_kEx_check.Text = "D-Optimal (k-Exchange)";
            this.num_kEx_check.UseVisualStyleBackColor = true;
            // 
            // num_FullFactorial_check
            // 
            this.num_FullFactorial_check.AutoSize = true;
            this.num_FullFactorial_check.Location = new System.Drawing.Point(350, 20);
            this.num_FullFactorial_check.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_FullFactorial_check.Name = "num_FullFactorial_check";
            this.num_FullFactorial_check.Size = new System.Drawing.Size(107, 20);
            this.num_FullFactorial_check.TabIndex = 11;
            this.num_FullFactorial_check.Text = "Full-Factorial";
            this.num_FullFactorial_check.UseVisualStyleBackColor = true;
            // 
            // num_CentralComposite_check
            // 
            this.num_CentralComposite_check.AutoSize = true;
            this.num_CentralComposite_check.Location = new System.Drawing.Point(177, 20);
            this.num_CentralComposite_check.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_CentralComposite_check.Name = "num_CentralComposite_check";
            this.num_CentralComposite_check.Size = new System.Drawing.Size(135, 20);
            this.num_CentralComposite_check.TabIndex = 10;
            this.num_CentralComposite_check.Text = "CentralComposite";
            this.num_CentralComposite_check.UseVisualStyleBackColor = true;
            // 
            // num_BoxBehnken_check
            // 
            this.num_BoxBehnken_check.AutoSize = true;
            this.num_BoxBehnken_check.Location = new System.Drawing.Point(8, 21);
            this.num_BoxBehnken_check.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_BoxBehnken_check.Name = "num_BoxBehnken_check";
            this.num_BoxBehnken_check.Size = new System.Drawing.Size(106, 20);
            this.num_BoxBehnken_check.TabIndex = 9;
            this.num_BoxBehnken_check.Text = "BoxBehnken";
            this.num_BoxBehnken_check.UseVisualStyleBackColor = true;
            // 
            // perfInfGridView
            // 
            this.perfInfGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.perfInfGridView.Location = new System.Drawing.Point(7, 21);
            this.perfInfGridView.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.perfInfGridView.Name = "perfInfGridView";
            this.perfInfGridView.Size = new System.Drawing.Size(1199, 384);
            this.perfInfGridView.TabIndex = 9;
            // 
            // pim_group
            // 
            this.pim_group.Controls.Add(this.perfInfGridView);
            this.pim_group.Location = new System.Drawing.Point(15, 345);
            this.pim_group.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pim_group.Name = "pim_group";
            this.pim_group.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pim_group.Size = new System.Drawing.Size(1212, 411);
            this.pim_group.TabIndex = 4;
            this.pim_group.TabStop = false;
            this.pim_group.Text = "Performance-Influence Model";
            // 
            // nfpSelection
            // 
            this.nfpSelection.FormattingEnabled = true;
            this.nfpSelection.Location = new System.Drawing.Point(15, 135);
            this.nfpSelection.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.nfpSelection.Name = "nfpSelection";
            this.nfpSelection.Size = new System.Drawing.Size(110, 44);
            this.nfpSelection.TabIndex = 9;
            this.nfpSelection.SelectedIndexChanged += new System.EventHandler(this.nfpSelection_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 117);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "NFP:";
            // 
            // MlSettings_Box
            // 
            this.MlSettings_Box.Controls.Add(this.mlSettingsPanel);
            this.MlSettings_Box.Location = new System.Drawing.Point(861, 7);
            this.MlSettings_Box.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MlSettings_Box.Name = "MlSettings_Box";
            this.MlSettings_Box.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MlSettings_Box.Size = new System.Drawing.Size(366, 338);
            this.MlSettings_Box.TabIndex = 11;
            this.MlSettings_Box.TabStop = false;
            this.MlSettings_Box.Text = "Machine-Learning Settings";
            // 
            // mlSettingsPanel
            // 
            this.mlSettingsPanel.AutoScroll = true;
            this.mlSettingsPanel.Location = new System.Drawing.Point(8, 21);
            this.mlSettingsPanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.mlSettingsPanel.Name = "mlSettingsPanel";
            this.mlSettingsPanel.Size = new System.Drawing.Size(352, 311);
            this.mlSettingsPanel.TabIndex = 0;
            // 
            // readMeasurements
            // 
            this.readMeasurements.Location = new System.Drawing.Point(15, 70);
            this.readMeasurements.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.readMeasurements.Name = "readMeasurements";
            this.readMeasurements.Size = new System.Drawing.Size(111, 38);
            this.readMeasurements.TabIndex = 12;
            this.readMeasurements.Text = "Read Measurements";
            this.readMeasurements.UseVisualStyleBackColor = true;
            this.readMeasurements.Click += new System.EventHandler(this.readMeasurements_Click);
            // 
            // cleanButton
            // 
            this.cleanButton.Location = new System.Drawing.Point(14, 195);
            this.cleanButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cleanButton.Name = "cleanButton";
            this.cleanButton.Size = new System.Drawing.Size(86, 38);
            this.cleanButton.TabIndex = 13;
            this.cleanButton.Text = "clean Sampling";
            this.cleanButton.UseVisualStyleBackColor = true;
            this.cleanButton.Click += new System.EventHandler(this.cleanButton_Click);
            // 
            // readVarModel
            // 
            this.readVarModel.Location = new System.Drawing.Point(15, 7);
            this.readVarModel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.readVarModel.Name = "readVarModel";
            this.readVarModel.Size = new System.Drawing.Size(111, 38);
            this.readVarModel.TabIndex = 14;
            this.readVarModel.Text = "Read Variability Model";
            this.readVarModel.UseVisualStyleBackColor = true;
            this.readVarModel.Click += new System.EventHandler(this.readVarModel_Click);
            // 
            // LearnAllMeasurements
            // 
            this.LearnAllMeasurements.Location = new System.Drawing.Point(519, 291);
            this.LearnAllMeasurements.Name = "LearnAllMeasurements";
            this.LearnAllMeasurements.Size = new System.Drawing.Size(151, 54);
            this.LearnAllMeasurements.TabIndex = 15;
            this.LearnAllMeasurements.Text = "Learn with all measurements";
            this.LearnAllMeasurements.UseVisualStyleBackColor = true;
            this.LearnAllMeasurements.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(23, 285);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 54);
            this.button1.TabIndex = 15;
            this.button1.Text = "Export selected model";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PrintConfigsGroupBox
            // 
            this.PrintConfigsGroupBox.Controls.Add(this.PrintconfigsButton);
            this.PrintConfigsGroupBox.Controls.Add(this.PostfixLabel);
            this.PrintConfigsGroupBox.Controls.Add(this.PrefixLabel);
            this.PrintConfigsGroupBox.Controls.Add(this.PostFixTextBox);
            this.PrintConfigsGroupBox.Controls.Add(this.PrefixTextBox);
            this.PrintConfigsGroupBox.Location = new System.Drawing.Point(675, 7);
            this.PrintConfigsGroupBox.Name = "PrintConfigsGroupBox";
            this.PrintConfigsGroupBox.Size = new System.Drawing.Size(176, 204);
            this.PrintConfigsGroupBox.TabIndex = 16;
            this.PrintConfigsGroupBox.TabStop = false;
            this.PrintConfigsGroupBox.Text = "Print Configurations";
            // 
            // PrintconfigsButton
            // 
            this.PrintconfigsButton.Location = new System.Drawing.Point(36, 127);
            this.PrintconfigsButton.Name = "PrintconfigsButton";
            this.PrintconfigsButton.Size = new System.Drawing.Size(103, 51);
            this.PrintconfigsButton.TabIndex = 4;
            this.PrintconfigsButton.Text = "Printconfigs";
            this.PrintconfigsButton.UseVisualStyleBackColor = true;
            this.PrintconfigsButton.Click += new System.EventHandler(this.PrintconfigsButton_Click);
            // 
            // PostfixLabel
            // 
            this.PostfixLabel.AutoSize = true;
            this.PostfixLabel.Location = new System.Drawing.Point(6, 88);
            this.PostfixLabel.Name = "PostfixLabel";
            this.PostfixLabel.Size = new System.Drawing.Size(52, 16);
            this.PostfixLabel.TabIndex = 3;
            this.PostfixLabel.Text = "Postfix:";
            // 
            // PrefixLabel
            // 
            this.PrefixLabel.AutoSize = true;
            this.PrefixLabel.Location = new System.Drawing.Point(6, 36);
            this.PrefixLabel.Name = "PrefixLabel";
            this.PrefixLabel.Size = new System.Drawing.Size(47, 16);
            this.PrefixLabel.TabIndex = 2;
            this.PrefixLabel.Text = "Prefix:";
            // 
            // PostFixTextBox
            // 
            this.PostFixTextBox.Location = new System.Drawing.Point(76, 82);
            this.PostFixTextBox.Name = "PostFixTextBox";
            this.PostFixTextBox.Size = new System.Drawing.Size(100, 25);
            this.PostFixTextBox.TabIndex = 1;
            // 
            // PrefixTextBox
            // 
            this.PrefixTextBox.Location = new System.Drawing.Point(76, 30);
            this.PrefixTextBox.Name = "PrefixTextBox";
            this.PrefixTextBox.Size = new System.Drawing.Size(100, 25);
            this.PrefixTextBox.TabIndex = 0;
            // 
            // PerformancePrediction_Frame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1237, 877);
            this.Controls.Add(this.PrintConfigsGroupBox);
            this.Controls.Add(this.LearnAllMeasurements);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.readVarModel);
            this.Controls.Add(this.cleanButton);
            this.Controls.Add(this.readMeasurements);
            this.Controls.Add(this.MlSettings_Box);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nfpSelection);
            this.Controls.Add(this.pim_group);
            this.Controls.Add(this.expDasign_group);
            this.Controls.Add(this.StartLearningButton);
            this.Controls.Add(this.LogGroup);
            this.Controls.Add(this.BinarySamplingGroup);
            this.Font = new System.Drawing.Font("Liberation Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "PerformancePrediction_Frame";
            this.Text = "SPL Conqueror: Performance Prediction";
            this.BinarySamplingGroup.ResumeLayout(false);
            this.BinarySamplingGroup.PerformLayout();
            this.LogGroup.ResumeLayout(false);
            this.LogGroup.PerformLayout();
            this.expDasign_group.ResumeLayout(false);
            this.expDasign_group.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.perfInfGridView)).EndInit();
            this.pim_group.ResumeLayout(false);
            this.MlSettings_Box.ResumeLayout(false);
            this.PrintConfigsGroupBox.ResumeLayout(false);
            this.PrintConfigsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox BinarySamplingGroup;
        private System.Windows.Forms.CheckBox negOW;
        private System.Windows.Forms.CheckBox PW;
        private System.Windows.Forms.CheckBox OW;
        private System.Windows.Forms.GroupBox LogGroup;
        private System.Windows.Forms.Button StartLearningButton;
        private System.Windows.Forms.GroupBox expDasign_group;
        private System.Windows.Forms.TextBox num_oneFactorAtATime_num_Text;
        private System.Windows.Forms.Label num_oneFactorAtATime_num_Label;
        private System.Windows.Forms.CheckBox num_oneFactorAtATime_Box;
        private System.Windows.Forms.TextBox num_rand_seed_Text;
        private System.Windows.Forms.Label num_rand_seed_Label;
        private System.Windows.Forms.TextBox num_Plackett_n_Box;
        private System.Windows.Forms.Label num_Plackett_n_Label;
        private System.Windows.Forms.TextBox num_Plackett_Level_Box;
        private System.Windows.Forms.Label num_Plackett_Level_Label;
        private System.Windows.Forms.TextBox num_hyper_percent_text;
        private System.Windows.Forms.Label num_hyper_percent_Label;
        private System.Windows.Forms.TextBox num_random_n_Text;
        private System.Windows.Forms.Label num_rand_n_Label;
        private System.Windows.Forms.TextBox num_kEx_k_Box;
        private System.Windows.Forms.Label num_kEx_k_Label;
        private System.Windows.Forms.TextBox num_kEx_n_Box;
        private System.Windows.Forms.Label num_kEx_n_Label;
        private System.Windows.Forms.CheckBox num_hyperSampling_check;
        private System.Windows.Forms.CheckBox num_randomSampling_num;
        private System.Windows.Forms.CheckBox num_PlackettBurman_check;
        private System.Windows.Forms.CheckBox num_kEx_check;
        private System.Windows.Forms.CheckBox num_FullFactorial_check;
        private System.Windows.Forms.CheckBox num_CentralComposite_check;
        private System.Windows.Forms.CheckBox num_BoxBehnken_check;
        private System.Windows.Forms.DataGridView perfInfGridView;
        private System.Windows.Forms.GroupBox pim_group;
        private System.Windows.Forms.CheckedListBox nfpSelection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox MlSettings_Box;
        private System.Windows.Forms.Panel mlSettingsPanel;
        private System.Windows.Forms.Button readMeasurements;
        private System.Windows.Forms.Button cleanButton;
        public System.Windows.Forms.TextBox LogBox;
        private System.Windows.Forms.Button readVarModel;
        private System.Windows.Forms.CheckBox binWholePop;
        private System.Windows.Forms.Button LearnAllMeasurements; private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox PrintConfigsGroupBox;
        private System.Windows.Forms.Button PrintconfigsButton;
        private System.Windows.Forms.Label PostfixLabel;
        private System.Windows.Forms.Label PrefixLabel;
        private System.Windows.Forms.TextBox PostFixTextBox;
        private System.Windows.Forms.TextBox PrefixTextBox;
        private CheckBox twise;
        private Label label2;
        private CheckBox binRandom;
        private TextBox twiseParam;
        private TextBox binRandomSeed;
        private Label label3;
        private TextBox numConfigsParam;
        private Label numConfigs;
    }
}

